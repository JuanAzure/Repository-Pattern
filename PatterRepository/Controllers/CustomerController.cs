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
    [Route("api/Customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        #region Propiedades
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public CustomerController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        #endregion


        [HttpGet]
        public async Task<IActionResult> GetAllCategorias()
        {
            var customer = await _repository.Customer.GetAllICustomerAsync(trackChanges: false);
            if (customer == null)
            {
                _logger.LogInfo($"El objecto Categorias no contiene datos. {customer.Count()}");
                return NotFound();
            }
            //var categoriasDto = _mapper.Map<IEnumerable<CategoriaDto>>(categorias);
            //_logger.LogInfo($"Returning {categoriasDto.Count()} Categoria.");
            return Ok(customer);
        }
    }
}
