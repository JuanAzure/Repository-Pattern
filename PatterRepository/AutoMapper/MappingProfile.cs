using AutoMapper;
using Entities.DataTransferObjects;
using Entities.DataTransferObjects.Order;
using Entities.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace PatterRepository
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {


            #region GET: Ventas - Detalles     


            //GET: Ventas 
            //Se obtiene unicamente todas las ventas(Maestro ventas).
            CreateMap<Order, OrdersGetDto>()
            .ForMember(O => O.Customer, opt => opt.MapFrom(c => c.Customer.Name));

            //GET: Ventas y Detalles
            //Se obtiene las ventas por ID 
            CreateMap<Order, OrderDto>()
                .ForMember(O => O.Customer, opt => opt.MapFrom(c => c.Customer.Name));

            // Obtiene detalles de Ventas.
            CreateMap<OrderItems, OrderItemsDto>()
            .ForMember(i => i.Item, opt => opt.MapFrom(o => o.Item.Name))
            .ForMember(p => p.Price, opt => opt.MapFrom(o => o.Item.Price))
            .ForMember(t => t.Total, opt => opt.MapFrom(o => o.Quantity * o.Item.Price));


            #endregion


            #region //POST: Ventas - Detalles
            //POST: Ventas y Detalles

            //Crear venta y detalles.
            CreateMap<OrderForCreationDto, Order>();
            CreateMap<OrderItemsForCreationDto, OrderItems>();
              

            #endregion



            #region //PUT: Ventas - Detalles

            //Actualiza venta y detalles.

            CreateMap<OrderForUpdateDto, Order>()
              .ForMember(or => or.OrderItems, opt => opt.Ignore())
                .AfterMap((orderUpdate, orderDB) =>
                {
                    var orderIds = orderUpdate.orderItems.Select(item => item.OrderItemID).ToList();
                    ///
                    var itemsUpdate = orderDB.OrderItems.Where(item => orderIds.Contains(item.OrderItemID) && item.OrderItemID > 0).ToList();
                    foreach (var itemU in itemsUpdate)
                    {
                        itemU.OrderItemID = orderUpdate.orderItems.Where(i => i.OrderItemID ==itemU.OrderItemID).First().OrderItemID;
                        itemU.OrderID = orderUpdate.orderItems.Where(i => i.OrderID == itemU.OrderID).First().OrderID;
                        itemU.ItemID = orderUpdate.orderItems.Where(i => i.OrderItemID == itemU.OrderItemID).First().ItemID;
                        itemU.Quantity = orderUpdate.orderItems.Where(i => i.OrderItemID == itemU.OrderItemID).First().Quantity;
                    }

                    //DELETE: Ventas
                    // delete - ids = Array [1,3,5]
                    var ventasBorrar = orderDB.OrderItems.Where(dir => !orderIds.Contains(dir.OrderItemID)).ToList();
                    foreach (var ventB in ventasBorrar)
                        orderDB.OrderItems.Remove(ventB);
                });
            CreateMap<OrderItemsForUpdateDto, OrderItems>();
            #endregion


      



            #region Mapeo Objeto Categoria

            CreateMap<Categoria, CategoriaDto>()
                .ForMember(x => x.categoriaId, opt => opt.MapFrom(c => c.CategoriaId));
            CreateMap<CategoriaForCreationDto, Categoria>();

            CreateMap<CategoriaForUpdateDto, Categoria>();
                //.ForMember(ca => ca.Articulos, opt => opt.Ignore())

                //   .AfterMap((clienteCRUD, clienteDB) =>
                //   {
                //       var ids = clienteCRUD.Articulos.Select(dir => dir.ArticuloId).ToList();

                //       // update
                //       var articulosActualizar = clienteDB.Articulos.Where(dir => ids.Contains(dir.Id) && dir.Id > 0).ToList();
                //       foreach (var dirU in articulosActualizar)
                //       {
                //           dirU.CategoriaId = clienteCRUD.Articulos.Where(cat => cat.ArticuloId == dirU.Id).First().CategoriaId;
                //           dirU.Codigo = clienteCRUD.Articulos.Where(cat => cat.ArticuloId == dirU.Id).First().Codigo;
                //           dirU.Nombre = clienteCRUD.Articulos.Where(cat => cat.ArticuloId == dirU.Id).First().Nombre;
                //           dirU.PrecioVenta = clienteCRUD.Articulos.Where(cat => cat.ArticuloId == dirU.Id).First().PrecioVenta;
                //           dirU.Stock = clienteCRUD.Articulos.Where(cat => cat.ArticuloId == dirU.Id).First().Stock;
                //           dirU.Descripcion = clienteCRUD.Articulos.Where(cat => cat.ArticuloId == dirU.Id).First().Descripcion;
                //           dirU.Condicion = clienteCRUD.Articulos.Where(cat => cat.ArticuloId == dirU.Id).First().Condicion;
                //       }

                //       // delete - ids = Array [1,3,5]
                //       var articulosBorrar = clienteDB.Articulos.Where(dir => !ids.Contains(dir.Id)).ToList();
                //       foreach (var artB in articulosBorrar)
                //           clienteDB.Articulos.Remove(artB);

                //   });
            #endregion

            #region Mapeo Objeto Persona
            CreateMap<Persona, PersonaDto>()
            .ForMember(x => x.personaId, opt => opt.MapFrom(p => p.Id));
            CreateMap<PersonaForCreationDto, Persona>();
            CreateMap<PersonaForUpdateDto, Persona>();
            #endregion





            CreateMap<AccountForCreationDto, Account>();
            CreateMap<Account, AccountDto>()
             .ForMember(c => c.Owner, opt => opt.MapFrom(x => x.owner.Name));


            CreateMap<Owner, OwnerDto>();
            CreateMap<OwnerForCreationDto, Owner>();
            CreateMap<OwnerForUpdateDto, Owner>();


            #region Mapeo Objeto Articulo

            CreateMap<Articulo, ArticuloDto>()
                .ForMember(c => c.Categoria, opt => opt.MapFrom(x => x.Categoria.Nombre));
            CreateMap<ArticuloForCreationDto, Articulo>();
            CreateMap<ArticuloForUpdateDto, Articulo>();

            #endregion
        }
    }
}

