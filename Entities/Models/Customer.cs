using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Customer")]
    public class Customer
    {        
        [Key]
        [Column("CustomerID")]
        public int CustomerID { get; set; }
        public string Name { get; set; }        
    }
}
