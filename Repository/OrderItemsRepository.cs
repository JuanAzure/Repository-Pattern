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
    public class OrderItemsRepository : RepositoryBase<OrderItems>, IOrderItemsRepository
    {

        public OrderItemsRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public async Task<OrderItems> GetByOrderIDAsync(long OrderID, bool trackChanges) =>

            await FindByCondition(or => or.OrderID.Equals(OrderID), trackChanges)
                 .Include(i => i.Item)
                .FirstOrDefaultAsync();
    }
}
