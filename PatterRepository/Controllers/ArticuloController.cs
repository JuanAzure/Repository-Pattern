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
        public async Task<IActionResult> CreateAccount(int categoriaId,
            [FromBody] ArticuloForCreationDto articulo)
        {
            if (articulo == null)
            {
                _logger.LogError("ArticuloForCreationDto object sent from client is null.");
                return BadRequest("ArticuloForCreationDto object is null");
            }

            var categoria = _repository.Categoria.GetCategoriaAsync(categoriaId, trackChanges: false);
            if (categoria == null)
            {
                _logger.LogInfo($"Categoria with id: {categoriaId} doesn't exist in the database.");
                return NotFound();
            }

            var articuloEntity = _mapper.Map<Articulo>(articulo);

            _repository.Articulo.CreateArticulo(categoriaId, articuloEntity);
            await _repository.SaveAsync();

            //var consulta = await _repository.Account.GetAccountWithDetailsAsync(accountEntity.Id, trackChanges: false);
            var ArticuloToReturn = _mapper.Map<ArticuloDto>(articuloEntity);

            return CreatedAtRoute("ArticuloId", new { categoriaId, id = ArticuloToReturn.Id }, ArticuloToReturn);
        }

    }
}