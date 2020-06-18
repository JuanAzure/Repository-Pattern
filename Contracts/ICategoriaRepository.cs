using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
  public interface ICategoriaRepository : IRepositoryBase<Categoria>
    {
        Task<IEnumerable<Categoria>> GetAllCategoriaAsync(bool trackChanges);
        Task<Categoria> GetCategoriaAsync(int categoriaId, bool trackChanges);        
        Task<IEnumerable<Categoria>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges);
        void CreateCategoria(Categoria categoria);
        void DeleteCategoria(Categoria categoria);
    }
}
