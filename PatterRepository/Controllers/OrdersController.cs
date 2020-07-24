using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects.Order;
using Entities.Models;
using LoggerService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PatterRepository.Controllers
{
    [Route("api/Orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        #region Propiedades
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public OrdersController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        #endregion


        [HttpGet]
        public async Task<ActionResult> GetAllOrders()
        {
            var getorders = await _repository.Order.GetAllOrderAsync(trackChanges: false);
            if (getorders == null)
            {
                _logger.LogInfo($"El objecto Categorias no contiene datos. {getorders.Count()}");
                return NotFound();
            }

            var orders = _mapper.Map<IEnumerable<GetOrdersDto>>(getorders);

            //_logger.LogInfo($"Returning {OrderDto.} Categoria.");
            _logger.LogInfo($"El objecto orders no contiene datos. {orders.Count()}");
            return Ok( new { orders });
        }


        [HttpGet("{OrderID}")]
        public async Task<ActionResult> GetByOrderID(long OrderID)
        {
            var getOrders = await _repository.Order.GetByOrderIDAsync(OrderID,trackChanges: false);
            if (getOrders == null)
            {
                _logger.LogInfo($"El objecto Categorias no contiene datos. {getOrders}");
                return NotFound();
            }
            var orders = _mapper.Map<OrderDto>(getOrders);
            _logger.LogInfo($"El objecto orders no contiene datos. {orders}");
            return Ok(new { orders });
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder([FromBody] OrderForCreationDto order)
        {
            if (order == null)
            {
                _logger.LogError("OrderForCreationDto object sent from client is null.");
                return BadRequest("OrderForCreationDto object is null");
            }

            var orderEntity = _mapper.Map<Order>(order);

            _repository.Order.CreateOrder(orderEntity);
            await _repository.SaveAsync();

            return Ok(new { order });
        }
    }
}
