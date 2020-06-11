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
    public class ArticuloRepository : RepositoryBase<Articulo>, IArticuloRepository
    {
        public ArticuloRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        public async Task<IEnumerable<Articulo>> GetAllArticuloAsync()
        {
            return

                 await FindAll(trackChanges:false)
                 .Include(ar => ar.Categoria)
                //.OrderBy(ar => ar.Descripcion)
                .ToListAsync();

        }

        public async Task<Articulo> GetArticuloByIdAsync(int ArticuloId)
        {
            return await FindCondition(owner => owner.IdArticulo.Equals(ArticuloId))
                .FirstOrDefaultAsync();
        }

        public async Task<Articulo> GetArticuloWithDetailsAsync(int ArticuloId)
        {
            return await FindCondition(ar => ar.IdArticulo.Equals(ArticuloId))
                .Include(ac => ac.Categoria.Nombre)
                .FirstOrDefaultAsync();
        }

        public void CreateArticulo(Articulo articulo)
        {
            Create(articulo);
        }

        public void UpdateArticulo(Articulo articulo)
        {
            Update(articulo);
        }

        public void DeleteArticulo(Articulo articulo)
        {
            Delete(articulo);
        }

    }
}
