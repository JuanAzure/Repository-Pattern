using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IItemRepository : IRepositoryBase<Item>
    {
        Task<IEnumerable<Item>> GetAllItemsAsync(bool trackChanges);

    }
}
