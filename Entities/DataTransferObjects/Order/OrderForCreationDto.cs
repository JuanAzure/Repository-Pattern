using System.Collections.Generic;
namespace Entities.DataTransferObjects.Order
{
    public class OrderForCreationDto
    {        
        public string OrderNo { get; set; }
        public int CustomerID { get; set; }
        public string PMethod { get; set; }
        public decimal GTotal { get; set; }        
        public IEnumerable<OrderItemsForCreationDto> orderItems { get; set; }

    }
}
