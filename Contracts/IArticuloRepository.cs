using Entities.Models;
using Entities.RequestFeatures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IArticuloRepository : IRepositoryBase<Articulo>
    {
        Task<IEnumerable<Articulo>> GetAllArticuloAsync(bool trackChanges);        
        Task<Articulo> GetArticuloAsync(int articuloId, bool trackChanges);
        Task<PagedList<Articulo>> GetArticuloCategoriaAsync(int categoriaId, ArticuloParameters articuloParameters, bool trackChanges);
        Task<Articulo> GetArticuloCategoriaAsync(int categoriaId, int id, bool trackChanges);
        Task<IEnumerable<Articulo>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges);
        void CreateArticulo(int categoriaId, Articulo articulo);
        void DeleteArticulo(Articulo articulo);
    }
}
