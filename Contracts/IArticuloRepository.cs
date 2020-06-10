using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
  public interface IArticuloRepository : IRepositoryBase<Articulo>
    {
        Task<IEnumerable<Articulo>> GetAllArticuloAsync();
        Task<Articulo> GetArticuloByIdAsync(int ArticuloId);
        Task<Articulo> GetArticuloWithDetailsAsync(int ArticuloId);
        void CreateArticulo(Articulo articulo);
        void UpdateArticulo(Articulo articulo);
        void DeleteArticulo(Articulo articulo);
    }
}
