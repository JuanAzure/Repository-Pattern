using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.Venta
{
   public class VentasGetDto
    {
        public int VentaId { get; set; }

        public int PersonaId { get; set; }

        public string Cliente { get; set; }                
        public string TipoComprobante { get; set; }
        public string SerieComprobante { get; set; }
        public string NumComprobante { get; set; }
        public DateTime FechaHora { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; }
    }
}
