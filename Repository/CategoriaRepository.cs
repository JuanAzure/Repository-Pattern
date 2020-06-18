using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class CategoriaRepository : RepositoryBase<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }
        public async Task<IEnumerable<Categoria>> GetAllCategoriaAsync(bool trackChanges) =>
         await FindAll(trackChanges).OrderBy(c => c.Descripcion).ToListAsync();        
            

        public Task<IEnumerable<Categoria>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges)
        {
            throw new NotImplementedException();
        }




        public async Task<Categoria> GetCategoriaAsync(int categoriaId, bool trackChanges) =>
         await FindByCondition(c => c.Id.Equals(categoriaId), trackChanges)
            //.Include(c => c.articulos)
            .SingleOrDefaultAsync();

        public void CreateArticulo(Categoria categoria) => Create(categoria);
        public void DeleteArticulo(Categoria categoria) => Delete(categoria);        
    }
}
