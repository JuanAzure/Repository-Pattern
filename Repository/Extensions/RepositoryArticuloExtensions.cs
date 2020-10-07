using Entities.Models;
using System.Linq;

namespace Repository.Extensions
{
    public static class  RepositoryArticuloExtensions
    {
        public static IQueryable<Articulo> FilterArticulos(this IQueryable<Articulo> articulos, uint minStock, uint maxStock) 
            => articulos.Where(a => (a.Stock >= minStock && a.Stock <= maxStock));

        public static IQueryable<Articulo> Search (this IQueryable<Articulo> articulos, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return articulos;

            var lowerCaseterm = searchTerm.Trim().ToLower();

            return articulos.Where(a=> a.Nombre.ToLower().Contains(lowerCaseterm));
        }

    }
}
