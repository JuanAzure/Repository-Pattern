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

        public async Task<IEnumerable<Categoria>> GetAllCategoriaAsync()
        {
            return await FindAll(trackChanges:false)
               .OrderBy(ar => ar.Descripcion)
               .ToListAsync();
        }

        public async Task<Categoria> GetCategoriaByIdAsync(int CategoriaId, bool trackChanges)

            => await FindCondition(c => c.IdCategoria.Equals(CategoriaId), trackChanges).SingleOrDefaultAsync();
        //{
        //    return await FindCondition(owner => owner.IdCategoria.Equals(CategoriaId))
        //        .FirstOrDefaultAsync();
        //}

        public async Task<Categoria> GetCategoriaWithDetailsAsync(int CategoriaId, bool trackChanges) =>
            await FindCondition(c => c.IdCategoria.Equals(CategoriaId), trackChanges).FirstOrDefaultAsync();        

        public void CreateCategoria(Categoria categoria)
        {
            Create(categoria);
        }

        public void UpdateCategoria(Categoria categoria)
        {
            Update(categoria);
        }

        public void DeleteCategoria(Categoria categoria)
        {
            Delete(categoria);
        }

    }
}
