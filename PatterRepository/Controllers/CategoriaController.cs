using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using LoggerService;
using Microsoft.AspNetCore.Mvc;
using PatterRepository.ModelBinders;

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

        [HttpPost]
        public async Task<IActionResult> CreateCategoria([FromBody] CategoriaForCreationDto _categoria)
        {
            if (_categoria == null)
            {
                _logger.LogError("CategoriaForCreationDto object sent from client is null.");
                return BadRequest("CategoriaForCreationDto object is null");
            }

            var categoriaEntity = _mapper.Map<Categoria>(_categoria);

            _repository.Categoria.CreateCategoria(categoriaEntity);
            await _repository.SaveAsync();

            //var articuloCategoria = await _repository.Articulo.GetArticuloAsync(articuloEntity.Id, trackChanges: false);
            var CategoriaToReturn = _mapper.Map<CategoriaDto>(categoriaEntity);
            return CreatedAtRoute("CategoriaId", new { id = CategoriaToReturn.Id }, CategoriaToReturn);
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
            var ids = string.Join(",", categoriaCollectionToReturn.Select(c => c.Id));
            return CreatedAtRoute("CategoriaCollection", new { ids }, categoriaCollectionToReturn);

        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoria(int id, [FromBody] CategoriaForUpdateDto _categoria)
        {
            if (_categoria == null)
            {
                _logger.LogError("CategoriaForUpdateDto object sent from client is null.");
                return BadRequest("CategoriaForUpdateDto object is null");
            }

            var categoriaEntity = await _repository.Categoria.GetCategoriaAsync(id, trackChanges: true);
            if (categoriaEntity == null)
            {
                _logger.LogInfo($"Categoria with id: {id} doesn't exist in the database.");

                return NotFound();
            } 
            
            _mapper.Map(_categoria, categoriaEntity);
            await _repository.SaveAsync();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            var categoriaEntity = await _repository.Categoria.GetCategoriaAsync(id, trackChanges: false);

            if (categoriaEntity == null)
            {
                _logger.LogInfo($"Categoria with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _repository.Categoria.DeleteCategoria(categoriaEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
    }
}
