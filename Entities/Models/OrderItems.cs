using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{

    [Table("OrderItems")]
    public class OrderItems
    {
        [Key]
        [Column("OrderItemID")]
        public long OrderItemID { get; set; }

        [ForeignKey(nameof(Order))]
        public long OrderID { get; set; }
        public int ItemID { get; set; }
        public int Quantity { get; set; }
        public virtual  Order order { get; set; }
        public virtual Item Item { get; set; }
    }
}
