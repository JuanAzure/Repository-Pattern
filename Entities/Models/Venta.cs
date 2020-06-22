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
        [Column("VentaId")]
        public int Id { get; set; }
        [Required]
        public int ClienteId { get; set; }
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
        public ICollection<DetalleVenta> Detalles { get; set; }        
        public Persona Persona { get; set; }
    }
}
