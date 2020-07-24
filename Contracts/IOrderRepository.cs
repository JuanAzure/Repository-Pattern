using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IOrderRepository : IRepositoryBase<Order>
    {
        Task<IEnumerable<Order>> GetAllOrderAsync(bool trackChanges);
        Task<Order> GetByOrderIDAsync(long OrderID, bool trackChanges);
        void CreateOrder(Order order );
    }
}
