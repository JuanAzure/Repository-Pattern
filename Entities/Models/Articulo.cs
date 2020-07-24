using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Articulo")]
    public  class Articulo
    {
        [Key]
        [Column("ArticuloId")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Codigo is requerid")]
        [StringLength(100, ErrorMessage = "Codigo can`t be than 100 characters")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Nombre is requerid")]
        [StringLength(100, ErrorMessage = "Nombre can`t be than 100 characters")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "PrecioVenta is requerid")]
        [Column(TypeName = "decimal(11,2)")]
        public Decimal PrecioVenta { get; set; }

        [Required(ErrorMessage = "Stock is requerid")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Descripcion is requerid")]
        [StringLength(100, ErrorMessage = "Descripcion can`t be than 100 characters")]
        public string Descripcion { get; set; }
        public bool? Condicion { get; set; }

        [ForeignKey(nameof(Categoria))]
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
    }
}
