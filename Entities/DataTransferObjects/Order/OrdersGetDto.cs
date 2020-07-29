using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.Order
{
    public class OrdersGetDto
    {
        public long OrderID { get; set; }
        public string OrderNo { get; set; }
        public string PMethod { get; set; }
        public decimal GTotal { get; set; }
        public int CustomerID { get; set; }
        public string Customer { get; set; }
    }
}
