using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
  public interface ICategoriaRepository : IRepositoryBase<Categoria>
    {
        Task<IEnumerable<Categoria>> GetAllCategoriaAsync();
        Task<Categoria> GetCategoriaByIdAsync(int CategoriaId);
        Task<Categoria> GetCategoriaWithDetailsAsync(int CategoriaId);
        void CreateCategoria(Categoria categoria);
        void UpdateCategoria(Categoria categoria);
        void DeleteCategoria(Categoria categoria);
    }
}
