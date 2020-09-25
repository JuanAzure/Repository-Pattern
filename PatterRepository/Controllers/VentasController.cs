using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.DataTransferObjects.Venta;
using Entities.Models;
using LoggerService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PatterRepository.Controllers
{
    [Route("api/ventas")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        #region Propiedades
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public VentasController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        [HttpGet]
        public async Task<ActionResult> GetAllVentas()
        {
            var getventas = await _repository.Venta.GetAllVentaAsync(trackChanges: false);
            if (getventas == null)
            {
                _logger.LogInfo($"El objecto Ventas no contiene datos. {getventas.Count()}");
                return NotFound();
            }

            var ventas = _mapper.Map<IEnumerable<VentasGetDto>>(getventas);
            _logger.LogInfo($"El objecto ventas cantidad de registos {ventas.Count()}");

             return Ok(new { ventas });

 //           return Ok(ventas);
        }



        [HttpGet("{id:int}")]       
         public async Task<ActionResult> GetAllDetails(int id)
        {
            var ventaDetails = await _repository.Venta.GetByVentaDetailsAsync(id, trackChanges:false);

            if(ventaDetails==null)
            {
                _logger.LogInfo($"Venta with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var entityVentasDetails = _mapper.Map<VentaDto>(ventaDetails);
            _logger.LogInfo($"El objecto Venta no contiene datos. {entityVentasDetails}");
            return Ok(new {Ventas= entityVentasDetails });                
        }

        [HttpPost]
        public async Task<ActionResult> CreateVenta([FromBody] VentaForCreationDto venta)
        {
            if (venta == null)
            {
                _logger.LogError("VentaForCreationDto object sent from client is null.");
                return BadRequest("OrderForCreationDto object is null");
            }


            if (venta.detalleVentas==null)
            {
                _logger.LogError("La venta no contiene detalles");
                return BadRequest($"La venta no contien detalles");
            }
            var ventaEntity = _mapper.Map<Venta>(venta);

            _repository.Venta.CreateVenta(ventaEntity);
            await _repository.SaveAsync();

            return Ok(new { venta });
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVenta(int id, [FromBody] VentaForUpdateDto ventaForUpdate)
        {
            if (ventaForUpdate == null)
            {
                _logger.LogError("ventaForUpdateDto object sent from client is null.");
                return BadRequest("ventaForUpdateDto object is null");
            }

            var ventaEntity = await _repository.Venta.GetVentaByIdAsync(id, trackChanges: true);
            if (ventaEntity == null)
            {
                _logger.LogInfo($"Venta with id: {id} doesn't exist in the database.");

                return NotFound();
            }

            _mapper.Map(ventaForUpdate, ventaEntity);
            await _repository.SaveAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVenta(int id)
        {
            var ventaEntity = await _repository.Venta.GetVentaByIdAsync(id, trackChanges: false);

            if (ventaEntity == null)
            {
                _logger.LogInfo($"Venta with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _repository.Venta.DeleteVenta(ventaEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
 




  
    }
}
