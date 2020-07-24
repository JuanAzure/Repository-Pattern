using Entities.Models;
using System.Collections.Generic;

namespace Entities.DataTransferObjects.Order
{
    public class OrderDto
    {
        public long OrderID { get; set; }
        public string OrderNo { get; set; }        
        public string PMethod { get; set; }
        public decimal GTotal { get; set; }
        public int CustomerID { get; set; }
        public string Customer { get; set; }
        public IEnumerable<OrderItemsDto> orderItems { get; set; }
    }
}
