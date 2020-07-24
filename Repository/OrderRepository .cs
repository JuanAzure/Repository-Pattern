using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {

        public OrderRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public async Task<IEnumerable<Order>> GetAllOrderAsync(bool trackChanges) =>

        await FindAll(trackChanges)
            .Include(c => c.Customer)
            .Include(o => o.OrderItems)
            .OrderByDescending(o => o.GTotal)
            .ToListAsync();

        public async Task<Order> GetByOrderIDAsync(long OrderID, bool trackChanges) =>
              await FindByCondition(or => or.OrderID.Equals(OrderID), trackChanges)
                  .Include(c => c.Customer)
                 .Include(o => o.OrderItems).ThenInclude(x => x.Item)
                .FirstOrDefaultAsync();

        public void CreateOrder(Order order) => Create(order);
    }
}
