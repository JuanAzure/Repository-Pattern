using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class ArticuloRepository : RepositoryBase<Articulo>, IArticuloRepository
    {
        public ArticuloRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public async Task<IEnumerable<Articulo>> GetAllArticuloAsync(bool trackChanges) =>
            await FindAll(trackChanges).OrderBy(c => c.Nombre)
            .Include(c => c.Categoria)
            .ToListAsync();


        public async Task<PagedList<Articulo>> GetArticuloCategoriaAsync(int categoriaId, ArticuloParameters articuloParameters, bool trackChanges)
        {
           var articulos =  await FindByCondition(c => c.CategoriaId.Equals(categoriaId) && (
                                                  c.Stock >= articuloParameters.MinStock && c.Stock <=                                             articuloParameters.MaxStock), trackChanges)                                   
                                                  .OrderBy(c => c.ArticuloId)
                                                  .ToListAsync();
            return PagedList<Articulo>
                 .ToPagedList(articulos, articuloParameters.PageNumber, articuloParameters.PageSize);
        }

        public async Task<IEnumerable<Articulo>> GetArticuloStock(int categoriaId, ArticuloParameters articuloParameters, bool trackChanges)
        {
            var articulos = await FindByCondition(c => c.CategoriaId.Equals(categoriaId) && (
                                                  c.Stock >= articuloParameters.MinStock && c.Stock <= articuloParameters.MaxStock), trackChanges)
                                                   .OrderBy(c => c.Stock)
                                                   .ToListAsync();
            return articulos;
        }

        public async Task<Articulo> GetArticuloAsync(int articuloId, bool trackChanges) =>

           await FindByCondition(c => c.ArticuloId.Equals(articuloId), trackChanges)
            .Include(c => c.Categoria)
            .SingleOrDefaultAsync();

        public async Task<IEnumerable<Articulo>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges) =>
        await FindByCondition(x => ids.Contains(x.ArticuloId), trackChanges).ToListAsync();
        public void CreateArticulo(int categoriaId, Articulo articulo)
        {
            articulo.CategoriaId = categoriaId;
            Create(articulo);
        }
        public void DeleteArticulo(Articulo articulo) => Delete(articulo);

        public async Task<Articulo> GetArticuloCategoriaAsync(int categoriaId, int id, bool trackChanges) =>
            await FindByCondition(e => e.CategoriaId.Equals(categoriaId) && e.ArticuloId.Equals(id), trackChanges).SingleOrDefaultAsync();



    }
}
