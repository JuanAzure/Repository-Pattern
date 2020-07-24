using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ItemRepository : RepositoryBase<Item>, IItemRepository
    {

        public ItemRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public async Task<IEnumerable<Item>> GetAllItemsAsync(bool trackChanges) =>
            await FindAll(trackChanges).OrderBy(i=>i.Name).ToListAsync();

        //public async Task<IEnumerable<Articulo>> GetAllArticuloAsync(bool trackChanges) =>
        //  await FindAll(trackChanges).OrderBy(c => c.Nombre)
        //  .Include(c => c.Categoria)
        //  .ToListAsync();

    }
}
