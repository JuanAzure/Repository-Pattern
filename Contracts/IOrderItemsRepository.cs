using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IOrderItemsRepository : IRepositoryBase<OrderItems>
    {
        Task<OrderItems> GetByOrderIDAsync(long OrderID, bool trackChanges);
    }
}
