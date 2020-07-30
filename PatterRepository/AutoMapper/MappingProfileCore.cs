using AutoMapper;
using Entities.DataTransferObjects;
using Entities.DataTransferObjects.Venta;
using Entities.DataTransferObjects.Venta.DetalleVenta;
using Entities.Models;
using System.Linq;

namespace PatterRepository.AutoMapper
{
    public class MappingProfileCore:Profile
    {


        public MappingProfileCore()
        {

            #region GET: Ventas - Detalles     


            //GET: Ventas 
            //Se obtiene unicamente todas las ventas(Maestro ventas).
            CreateMap<Venta, VentasGetDto>()
            .ForMember(c => c.Id, opt => opt.MapFrom(v => v.VentaId))
            .ForMember(c => c.Cliente, opt => opt.MapFrom(c =>string.Join(' ',c.Persona.Nombre,", ",c.Persona.TipoDocumento,": ",c.Persona.NumDocumento,", Email: ",c.Persona.Email)));
            #endregion


            #region //POST: Ventas - Detalles
            //POST: Ventas y Detalles

            //Crear venta y detalles.
            CreateMap<VentaForCreationDto, Venta>();
            CreateMap<DetalleVentaForCreation, DetalleVenta>();

            #endregion



            #region //PUT: Ventas - Detalles

            //Actualiza venta y detalles.

            CreateMap<VentaForUpdateDto, Venta>()
              .ForMember(vta=> vta.DetalleVenta, opt => opt.Ignore())
                .AfterMap((ventaUpdate, ventaDB) =>
                {
                    //var orderIds = orderUpdate.orderItems.Select(item => item.OrderItemID).ToList();

                    var ventaIds = ventaUpdate.detalleVenta.Select(art => art.DetalleVentaId).ToList();
                    ///
                    var articuloUpdate = ventaDB.DetalleVenta.Where(art => ventaIds.Contains(art.DetalleVentaId) && art.DetalleVentaId > 0).ToList();
                    ///

                    foreach (var artU in articuloUpdate)
                    {
                        artU.DetalleVentaId = ventaUpdate.detalleVenta.Where(i => i.DetalleVentaId == artU.DetalleVentaId).First().DetalleVentaId;
                        artU.VentaId = ventaUpdate.detalleVenta.Where(i => i.VentaId == artU.VentaId).First().VentaId;

                        artU.ArticuloId = ventaUpdate.detalleVenta.Where(i => i.DetalleVentaId == artU.DetalleVentaId).First().ArticuloId;

                        artU.Cantidad = ventaUpdate.detalleVenta.Where(i => i.DetalleVentaId == artU.DetalleVentaId).First().Cantidad;
                        artU.Precio = ventaUpdate.detalleVenta.Where(i => i.DetalleVentaId == artU.DetalleVentaId).First().Precio;
                        artU.Descuento = ventaUpdate.detalleVenta.Where(i => i.DetalleVentaId == artU.DetalleVentaId).First().Descuento;


                    }

                    //DELETE: Ventas
                    // delete - ids = Array [1,3,5]
                    //var ventasBorrar = orderDB.OrderItems.Where(dir => !orderIds.Contains(dir.OrderItemID)).ToList();
                    //foreach (var ventB in ventasBorrar)
                    //    orderDB.OrderItems.Remove(ventB);
                });
            CreateMap<DetalleVentaForUpdate, DetalleVenta>();
            #endregion
        }

    }
}
