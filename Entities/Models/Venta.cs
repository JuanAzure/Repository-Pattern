using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("Venta")]
    public class Venta
    {
        
        [Key]        
        public int VentaId { get; set; }
        [Required]
        public int PersonaId { get; set; }
        [Required]
        public int UsuarioId { get; set; }
        [Required]
        public string TipoComprobante { get; set; }
        public string SerieComprobante { get; set; }
        [Required]
        public string NumComprobante { get; set; }
        [Required]
        public DateTime FechaHora { get; set; }
        [Required]
        public decimal Impuesto { get; set; }
        [Required]
        public decimal Total { get; set; }
        [Required]
        public string Estado { get; set; }
        public ICollection<DetalleVenta> DetalleVenta{ get; set; }        
        public Persona Persona { get; set; }
    }
}
