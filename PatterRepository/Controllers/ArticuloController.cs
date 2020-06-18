using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using LoggerService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> CreateArticulo(int categoriaId, [FromBody]  ArticuloForCreationDto articulo)
        {
            if (articulo == null)
            {
                _logger.LogError("ArticuloForCreationDto object sent from client is null.");
                return BadRequest("ArticuloForCreationDto object is null");
            }

            var categoria = await _repository.Categoria.GetCategoriaAsync(categoriaId, trackChanges: false);
            if (categoria == null)
            {
                _logger.LogInfo($"Categoria with id: {categoriaId} doesn't exist in the database.");
                return NotFound();
            }

            var articuloEntity = _mapper.Map<Articulo>(articulo);

            _repository.Articulo.CreateArticulo(categoriaId, articuloEntity);
            await _repository.SaveAsync();

            var articuloCategoria = await _repository.Articulo.GetArticuloAsync(articuloEntity.Id, trackChanges: false);
            var ArticuloToReturn = _mapper.Map<ArticuloDto>(articuloCategoria);
            return CreatedAtRoute("ArticuloId", new { id = ArticuloToReturn.Id }, ArticuloToReturn);
        }

        [Route("{id}/categorias/{categoriaId}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteArticuloForCategoria(int categoriaId, int id)
        {
            var categoria = await _repository.Categoria.GetCategoriaAsync(categoriaId, trackChanges: false);
            if (categoria == null)
            {
                _logger.LogInfo($"Categoria with id: {categoriaId} doesn't exist in the  database.");

                return NotFound();
            }
            var ArticuloForCategoria = await _repository.Articulo.GetArticuloCategoriaAsync(categoriaId, id, trackChanges: false);

            if (ArticuloForCategoria == null)
            {
                _logger.LogInfo($"Employee with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _repository.Articulo.DeleteArticulo(ArticuloForCategoria);
            await _repository.SaveAsync();
            return NoContent();
        }


        [Route("{id}/categorias/{categoriaId}")]
        [HttpPut]
        public async Task<IActionResult> UpdateArticuloForCategoria(int categoriaId, int id, [FromBody] ArticuloForUpdateDto articulo)
        {
            if (articulo == null)
            {
                _logger.LogError("ArticuloForUpdateDto object sent from client is null.");
                return BadRequest("ArticuloForUpdateDto object is null");
            }

            var categoria = await _repository.Categoria.GetCategoriaAsync(categoriaId, trackChanges: false);
            if (categoria == null)
            {
                _logger.LogInfo($"Categoria with id: {categoriaId} doesn't exist in the database.");

                return NotFound();
            }
            var articuloEntity = await _repository.Articulo.GetArticuloCategoriaAsync(categoriaId, id, trackChanges: true);

            if (articuloEntity == null)
            {
                _logger.LogInfo($"Articulo with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _mapper.Map(articulo, articuloEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
    }
}