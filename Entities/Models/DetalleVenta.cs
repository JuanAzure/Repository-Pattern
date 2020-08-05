using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("DetalleVenta")]
    public  class DetalleVenta
    {
        [Key]        
        public int DetalleVentaId { get; set; }

        [ForeignKey(nameof(Venta))]
        public int VentaId { get; set; }
        [Required]
        public int ArticuloId { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        public decimal Precio { get; set; }
        [Required]
        public decimal Descuento { get; set; }
        public virtual Venta Venta { get; set; }
        public virtual Articulo Articulo { get; set; }
    }
}
