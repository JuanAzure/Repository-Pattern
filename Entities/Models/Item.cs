using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{

    [Table("Item")]
    public class Item
    {
        [Key]
        [Column("ItemID")]
        public int ItemID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
