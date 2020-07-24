using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using LoggerService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PatterRepository.Controllers
{
    [Route("api/Items")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        #region Propiedades
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public ItemsController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        #endregion


        [HttpGet]
        public async Task<IActionResult> GetAllCategorias()
        {
            var items = await _repository.Item.GetAllItemsAsync(trackChanges: false);
            if (items == null)
            {
                _logger.LogInfo($"El objecto Categorias no contiene datos. {items.Count()}");
                return NotFound();
            }
            //var categoriasDto = _mapper.Map<IEnumerable<CategoriaDto>>(categorias);
            //_logger.LogInfo($"Returning {categoriasDto.Count()} Categoria.");
            return Ok(items);
        }
    }
}
