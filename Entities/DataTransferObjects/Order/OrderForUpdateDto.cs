using System.Collections.Generic;
namespace Entities.DataTransferObjects.Order
{
    public class OrderForUpdateDto
    {
      
       // public long OrderID { get; set; }
        public string OrderNo { get; set; }
        public int CustomerID { get; set; }
        public string PMethod { get; set; }
        public decimal GTotal { get; set; }        
        public IEnumerable<OrderItemsForUpdateDto> orderItems { get; set; }

    }
}
