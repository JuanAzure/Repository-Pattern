using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{

    [Table("Order")]
    public class Order
    {
        [Key]
        [Column("OrderID")]
        public long OrderID { get; set; }
        public string OrderNo { get; set; }
        public int  CustomerID { get; set; }
        public string PMethod { get; set; }
        public decimal GTotal { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderItems> OrderItems { get; set; }
    }
}
