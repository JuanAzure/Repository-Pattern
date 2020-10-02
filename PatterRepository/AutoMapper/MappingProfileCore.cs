using AutoMapper;
using Entities.DataTransferObjects;
using Entities.DataTransferObjects.Venta;
using Entities.DataTransferObjects.Venta.DetalleVenta;
using Entities.Models;
using System.Linq;

namespace PatterRepository.AutoMapper
{
    public class MappingProfileCore : Profile
    {


        public MappingProfileCore()
        {

            #region GET: Ventas - Detalles     


            //GET: Ventas 

            //Se obtiene unicamente todas las ventas(Maestro ventas).

            CreateMap<Venta, VentasGetDto>()
            .ForMember(c => c.VentaId, opt => opt.MapFrom(v => v.VentaId))
            .ForMember(c => c.PersonaId, opt => opt.MapFrom(p => p.Persona.Id))
            .ForMember(c => c.Cliente, opt => opt.MapFrom(c => string.Join(' ', c.Persona.Nombre)));

            CreateMap<Venta, VentaDto>();
            CreateMap<DetalleVenta, DetalleVentaDto>()
                .ForMember(p => p.Nombre, opt => opt.MapFrom(p => p.Articulo.Nombre))
                .ForMember(p => p.PrecioVenta, opt => opt.MapFrom(p => p.Precio))
                .ForMember(st => st.Total, opt => opt.MapFrom(c => c.Cantidad * c.Precio));         
            #endregion


            #region //POST:Se crea   Ventas - Detalles
            //POST: Ventas y Detalles

            //Crear venta y detalles.
            CreateMap<VentaForCreationDto, Venta>();
            CreateMap<DetalleVentaForCreation, DetalleVenta>()
              .ForMember(p => p.Precio, opt => opt.MapFrom(p => p.PrecioVenta));

            #endregion

            #region //PUT: Ventas - Detalles

            //Actualiza venta y detalles.

            CreateMap<VentaForUpdateDto, Venta>()
              .ForMember(vta => vta.DetalleVentas, opt => opt.Ignore())
                .AfterMap((ventaUpdate, ventaDB) =>
                {
                    var detalleIds = ventaUpdate.detalleVentas.Select(det => det.DetalleVentaId).ToList();

                    //Insert

                    var detallesAgregar = ventaUpdate.detalleVentas.Where(det => det.DetalleVentaId <= 0).ToList();


                    foreach (var detalleA in detallesAgregar)
                    {
                        ventaDB.DetalleVentas.Add(new DetalleVenta()
                        {
                            VentaId = detalleA.VentaId,
                            ArticuloId = detalleA.ArticuloId,
                            Cantidad = detalleA.Cantidad,
                            Precio = detalleA.PrecioVenta,
                            Descuento = detalleA.Descuento
                        });
                    }

                    //Update
                    var ventaIds = ventaUpdate.detalleVentas.Select(art => art.DetalleVentaId).ToList();
                    ///

                    var articuloUpdate = ventaDB.DetalleVentas.Where(art => ventaIds.Contains(art.DetalleVentaId) && art.DetalleVentaId > 0).ToList();
                    ///

                    foreach (var artU in articuloUpdate)
                    {
                        artU.DetalleVentaId = ventaUpdate.detalleVentas.Where(i => i.DetalleVentaId == artU.DetalleVentaId).First().DetalleVentaId;
                        artU.VentaId = ventaUpdate.detalleVentas.Where(i => i.VentaId == artU.VentaId).First().VentaId;

                        artU.ArticuloId = ventaUpdate.detalleVentas.Where(i => i.DetalleVentaId == artU.DetalleVentaId).First().ArticuloId;

                        artU.Cantidad = ventaUpdate.detalleVentas.Where(i => i.DetalleVentaId == artU.DetalleVentaId).First().Cantidad;
                        artU.Precio = ventaUpdate.detalleVentas.Where(i => i.DetalleVentaId == artU.DetalleVentaId).First().PrecioVenta;
                        artU.Descuento = ventaUpdate.detalleVentas.Where(i => i.DetalleVentaId == artU.DetalleVentaId).First().Descuento;
                    }

                    //DELETE: DetalleVentas
                    //delete - ids = Array[1, 3, 5]


                    var detallesBorrar = ventaDB.DetalleVentas.Where(det => !detalleIds.Contains(det.DetalleVentaId)).ToList();
                    foreach (var detB in detallesBorrar)
                        ventaDB.DetalleVentas.Remove(detB);
                });

            CreateMap<DetalleVentaForUpdate, DetalleVenta>();
            #endregion
        }

    }
}
