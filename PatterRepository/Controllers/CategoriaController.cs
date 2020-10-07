using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using LoggerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatterRepository.ModelBinders;
using Microsoft.AspNetCore.JsonPatch;
using PatterRepository.ActionFilters;
namespace PatterRepository.Controllers
{
    [Route("api/categorias")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        #region Propiedades
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        #endregion
        #region Constructor
        public CategoriaController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        #endregion
       [HttpGet]
        public async Task<IActionResult> GetAllCategorias()
        {
            var categorias = await _repository.Categoria.GetAllCategoriaAsync(trackChanges: false);
            if (categorias == null)
            {
                _logger.LogInfo($"El objecto Categorias no contiene datos. {categorias.Count()}");
                return NotFound();
            }
            var categoriasDto = _mapper.Map<IEnumerable<CategoriaDto>>(categorias);
            _logger.LogInfo($"Returning {categoriasDto.Count()} Categoria.");
            return Ok(categoriasDto);
        }

        [HttpGet("{id}", Name = "CategoriaId")]
        [ServiceFilter(typeof(ValidateCategoryExistsAttribute))]
        public async Task<IActionResult> GetCategoria(int id)
        {
            var categoria = await _repository.Categoria.GetCategoriaAsync(id, trackChanges: false);
            if (categoria == null)
            {
                _logger.LogInfo($"Articulo with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var categoriaDto = _mapper.Map<CategoriaDto>(categoria);
            _logger.LogInfo($"Returning Controller - Name:{nameof(GetCategoria)}");
            return Ok(categoriaDto);
        }
       
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateCategoria([FromBody] CategoriaForCreationDto _categoria)
        {
            var categoriaEntity = _mapper.Map<Categoria>(_categoria);
            _repository.Categoria.CreateCategoria(categoriaEntity);

            try
            {
                await _repository.SaveAsync();
            }
            catch (DbUpdateException e)
            {
                var error = e.InnerException.Message;
                if (error.Contains("UNIQUE KEY"))
                    _logger.LogError(error);
                return BadRequest("No se puede insertar una clave duplicada en el Nombre :" + categoriaEntity.Nombre);
            }
            var CategoriaToReturn = _mapper.Map<CategoriaDto>(categoriaEntity);
            return CreatedAtRoute("CategoriaId", new { id = CategoriaToReturn.categoriaId }, CategoriaToReturn);
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateCategoryExistsAttribute))]
        public async Task<IActionResult> UpdateCategoria(int id, [FromBody] CategoriaForUpdateDto _categoria)
        {
            var categoriaEntity = HttpContext.Items["categoria"] as Categoria;
            _mapper.Map(_categoria, categoriaEntity);
            try
            {
                await _repository.SaveAsync();
            }
            catch (DbUpdateException e)
            {
                var error = e.InnerException.Message;
                if (error.Contains("UNIQUE KEY"))
                    _logger.LogError(error);
                return BadRequest("No se puede insertar una clave duplicada en el Nombre :" + categoriaEntity.Nombre);
            }
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdateCategoria(int id, [FromBody] JsonPatchDocument<CategoriaForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }

            var categoriaEntity = await _repository.Categoria.GetCategoriaAsync(id, trackChanges: true);
            if (categoriaEntity == null)
            {
                _logger.LogInfo($"Categoria with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var categoriaPatch = _mapper.Map<CategoriaForUpdateDto>(categoriaEntity);

            patchDoc.ApplyTo(categoriaPatch);

            TryValidateModel(categoriaPatch);
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the patch document");
                return UnprocessableEntity(ModelState);
            }
            _mapper.Map(categoriaPatch, categoriaEntity);
            try
            {
                await _repository.SaveAsync();
            }
            catch (DbUpdateException e)
            {
                var error = e.InnerException.Message;
                if (error.Contains("UNIQUE KEY"))
                    _logger.LogError(error);
                return BadRequest("No se puede insertar una clave duplicada en el Nombre :" + categoriaEntity.Nombre);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateCategoryExistsAttribute))]
        public async Task<IActionResult> DeleteCategoria(int id)
        {            
            var categoriaEntity = HttpContext.Items["categoria"] as Categoria;
            _repository.Categoria.DeleteCategoria(categoriaEntity);
            await _repository.SaveAsync();
            return NoContent();
        }


        [HttpGet("collection/{ids}", Name = "CategoriaCollection")]
        public async Task<IActionResult> GetCategoriesCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<int> ids)
        {
            if (ids == null)
            {
                _logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }

            var categoriesEntities = await _repository.Categoria.GetByIdsAsync(ids, trackChanges: false);

            if (ids.Count() != categoriesEntities.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return NotFound();
            }
            var categoriesToReturn = _mapper.Map<IEnumerable<CategoriaDto>>(categoriesEntities);

            return Ok(categoriesToReturn);
        }

        [HttpPost("collection")]
        public async Task<IActionResult> CreateCategoriaCollection([FromBody]
          IEnumerable<CategoriaForCreationDto> categoriaCollection)
        {
            if (categoriaCollection == null)
            {
                _logger.LogError("Categoria collection sent from client is null.");
                return BadRequest("Categoria collection is null");
            }
            var categoriaEntities = _mapper.Map<IEnumerable<Categoria>>(categoriaCollection);

            foreach (var categoria in categoriaEntities)
            {
                _repository.Categoria.Create(categoria);
            }
            await _repository.SaveAsync();
            var categoriaCollectionToReturn =
           _mapper.Map<IEnumerable<CategoriaDto>>(categoriaEntities);
            var ids = string.Join(",", categoriaCollectionToReturn.Select(c => c.categoriaId));
            return CreatedAtRoute("CategoriaCollection", new { ids }, categoriaCollectionToReturn);

        }

    }
}
