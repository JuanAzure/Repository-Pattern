using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using LoggerService;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PatterRepository.ActionFilters;

namespace PatterRepository.Controllers
{
    [Route("api/Articulos")]
    [ApiController]
    public class ArticuloController : ControllerBase
    {
        #region Propiedades
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public ArticuloController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        [HttpGet]
        public async Task<IActionResult> GetArticulos()
        {
            var articulos = await _repository.Articulo.GetAllArticuloAsync(trackChanges: false);
            if (articulos == null)
            {
                _logger.LogInfo($"El objecto Articulo no contiene datos. {articulos.Count()}");
                return NotFound();
            }
            var articulosDto = _mapper.Map<IEnumerable<ArticuloDto>>(articulos);
            _logger.LogInfo($"Returning {articulosDto.Count()} Articulos.");
            return Ok(articulosDto);
        }

        [Route("{categoriaId}/categorias")]
        [HttpGet]
        public async Task<IActionResult> GetArticulosCategoria(int categoriaId, [FromQuery] ArticuloParameters articuloParameters)
        {
            if(!articuloParameters.ValidaStockRange)
            {
                return BadRequest("El stock máximo no puede ser menor que la stock mínima");
            }
            var articulos = await _repository.Articulo.GetArticuloCategoriaAsync(categoriaId, articuloParameters, trackChanges: false);
            if (articulos == null)
            {
                _logger.LogInfo($"El objecto Articulo no contiene datos. {articulos.Count()}");
                return NotFound();
            }

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(articulos.MetaData));

            var articulosDto = _mapper.Map<IEnumerable<ArticuloDto>>(articulos);
            _logger.LogInfo($"Returning {articulosDto.Count()} Articulos.");
            return Ok(articulosDto);
        }


        [HttpGet("{id}", Name = "ArticuloId")]
        public async Task<IActionResult> GetArticulo(int id)
        {
            var articulos = await _repository.Articulo.GetArticuloAsync(id, trackChanges: false);
            if (articulos == null)
            {
                _logger.LogInfo($"Articulo with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var articulosDto = _mapper.Map<ArticuloDto>(articulos);
            _logger.LogInfo($"Returning Controller - Name:{nameof(GetArticulo)} Articulos.");
            return Ok(articulosDto);
        }

        [Route("{categoriaId}/categorias")]
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateArticulo(int categoriaId, [FromBody] ArticuloForCreationDto _articulo)
        {
            var categoria = await _repository.Categoria.GetCategoriaAsync(categoriaId, trackChanges: false);
            if (categoria == null)
            {
                _logger.LogInfo($"Categoria with id: {categoriaId} doesn't exist in the database.");
                return NotFound();
            }
            var articuloEntity = _mapper.Map<Articulo>(_articulo);
            _repository.Articulo.CreateArticulo(categoriaId, articuloEntity);
            try
            {
                await _repository.SaveAsync();
            }
            catch (DbUpdateException e)
            {
                var error = e.InnerException.Message;
                if (error.Contains("UNIQUE KEY"))
                    _logger.LogError(error);
                return BadRequest("No se puede insertar una clave duplicada en el Nombre: " + "'" + articuloEntity.Nombre + "'");
            }

            var articuloCategoria = await _repository.Articulo.GetArticuloAsync(articuloEntity.ArticuloId, trackChanges: false);
            var ArticuloToReturn = _mapper.Map<ArticuloDto>(articuloCategoria);
            return CreatedAtRoute("ArticuloId", new { id = ArticuloToReturn.articuloId }, ArticuloToReturn);
        }

        [Route("{id}/categorias/{categoriaId}")]
        [HttpDelete]
        [ServiceFilter(typeof(ValidateArticuloExistsAttribute))]
        public async Task<IActionResult> DeleteArticuloForCategoria(int categoriaId, int id)
        {
            var ArticuloForCategoria = HttpContext.Items["articulo"] as Articulo;
            _repository.Articulo.DeleteArticulo(ArticuloForCategoria);
            await _repository.SaveAsync();
            return NoContent();
        }

        [Route("{id}/categorias/{categoriaId}")]
        [HttpPut]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateArticuloExistsAttribute))]
        public async Task<IActionResult> UpdateArticuloForCategoria(int categoriaId, int id, [FromBody] ArticuloForUpdateDto articulo)
        {
            var articuloEntity = HttpContext.Items["articulo"] as Articulo;
            _mapper.Map(articulo, articuloEntity);

            try
            {
                await _repository.SaveAsync();
            }
            catch (DbUpdateException e)
            {

                var error = e.InnerException.Message;
                if (error.Contains("UNIQUE KEY"))
                    _logger.LogError(error);
                return BadRequest("No se puede insertar una clave duplicada en el Nombre :" + articuloEntity.Nombre);
            }
            return NoContent();
        }


        [Route("{id}/categorias/{categoriaId}")]
        [HttpPatch]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateArticuloExistsAttribute))]
        public async Task<IActionResult> PatchUpdateArticulo(int categoriaId, int id, [FromBody] JsonPatchDocument<ArticuloForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }

            var articuloEntity = HttpContext.Items["articulo"] as Articulo;

            var articuloPatch = _mapper.Map<ArticuloForUpdateDto>(articuloEntity);

            patchDoc.ApplyTo(articuloPatch);

            TryValidateModel(articuloPatch);

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the patch document");
                return UnprocessableEntity(ModelState);
            }

            _mapper.Map(articuloPatch, articuloEntity);

            try
            {
                await _repository.SaveAsync();
            }
            catch (DbUpdateException e)
            {
                var error = e.InnerException.Message;
                if (error.Contains("UNIQUE KEY"))
                    _logger.LogError(error);
                return BadRequest("No se puede insertar una clave duplicada en el Nombre :" + articuloEntity.Nombre);
            }
            return NoContent();
        }
    }
}