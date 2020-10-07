using Entities.Models;
using Entities.RequestFeatures;
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
        Task<IEnumerable<Articulo>> GetArticuloStock(int categoriaId, ArticuloParameters articuloParameters, bool trackChanges);

        Task<PagedList<Articulo>> GetArticuloCategoriaAsync(int categoriaId, ArticuloParameters articuloParameters, bool trackChanges);
        Task<Articulo> GetArticuloCategoriaAsync(int categoriaId, int id, bool trackChanges);
        Task<IEnumerable<Articulo>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges);
        void CreateArticulo(int categoriaId, Articulo articulo);
        void DeleteArticulo(Articulo articulo);
    }
}
