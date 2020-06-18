using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
  public interface IArticuloRepository : IRepositoryBase<Articulo>
    {
        Task<IEnumerable<Articulo>> GetAllArticuloAsync(bool trackChanges);
        Task<Articulo> GetArticuloAsync(int articuloId, bool trackChanges);
        void CreateArticulo(int categoriaId, Articulo articulo);
        Task<IEnumerable<Articulo>> GetByIdsAsync(IEnumerable<int> ids, bool
       trackChanges);
        void DeleteArticulo(Articulo articulo);
    }
}
