using Entities.DataTransferObjects.Venta.DetalleVenta;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class VentaForCreationDto
    {        
            public int PersonaId { get; set; }
            public int UsuarioId { get; set; }            
            public string TipoComprobante { get; set; }
            public string SerieComprobante { get; set; }            
            public string NumComprobante { get; set; }
            public DateTime FechaHora { get; set; } = DateTime.Now;          
            public decimal Impuesto { get; set; }            
            public decimal Total { get; set; }
            public string Estado { get; set; } = "Confirmado";
           public IEnumerable<DetalleVentaForCreation> detalleVentas { get; set; }

    }
}
