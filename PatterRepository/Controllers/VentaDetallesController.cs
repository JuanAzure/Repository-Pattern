using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.DataTransferObjects.Venta;
using Entities.DataTransferObjects.Venta.DetalleVenta;
using Entities.Models;
using LoggerService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PatterRepository.Controllers
{
    [Route("api/VentaDetalles")]
    [ApiController]
    public class VentaDetallesController : ControllerBase
    {
        #region Propiedades
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public VentaDetallesController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        #endregion



        [HttpGet("{id:int}")]       
         public async Task<ActionResult> GetVentaDetalles(int id)
        {
            var ventaEntity = await _repository.Venta.GetVentaByIdAsync(id,trackChanges:false);
            if (ventaEntity == null)
            {
                _logger.LogInfo($"Venta with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            
            var ventaDetails = await _repository.DetalleVenta.GetVentaDetallesAsync(ventaEntity.VentaId, trackChanges:false);

            if(ventaDetails==null)
            {
                _logger.LogInfo($"Venta with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var entityDetalle = _mapper.Map<IEnumerable<DetalleVentaDto>>(ventaDetails);
            _logger.LogInfo($"El objecto Venta no contiene datos. {entityDetalle}");
            return Ok(new {detalles= entityDetalle });                
        }
  
    }
}
