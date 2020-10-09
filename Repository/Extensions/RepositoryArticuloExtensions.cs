using Entities.Models;
using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using Repository.Extensions.Utility;

namespace Repository.Extensions
{
    public static class RepositoryArticuloExtensions
    {
        public static IQueryable<Articulo> FilterArticulos(this IQueryable<Articulo> articulos, uint minStock, uint maxStock)
            => articulos.Where(a => (a.Stock >= minStock && a.Stock <= maxStock));

        public static IQueryable<Articulo> Search(this IQueryable<Articulo> articulos, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return articulos;

            var lowerCaseterm = searchTerm.Trim().ToLower();

            return articulos.Where(a => a.Nombre.ToLower().Contains(lowerCaseterm));

        }

        public static IQueryable<Articulo> Sort(this IQueryable<Articulo> articulos, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return articulos.OrderBy(e => e.Nombre);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Articulo>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return articulos.OrderBy(e => e.Nombre);

            return articulos.OrderBy(orderQuery);

        }
    }
}
