using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class VentaForCreationDto
    {
        public class Venta
        {
            public int Id { get; set; }            
            public int ClienteId { get; set; }
            public int UsuarioId { get; set; }            
            public string TipoComprobante { get; set; }
            public string SerieComprobante { get; set; }            
            public string NumComprobante { get; set; }            
            public DateTime FechaHora { get; set; }            
            public decimal Impuesto { get; set; }            
            public decimal Total { get; set; }            
            public string Estado { get; set; }                       
        }

    }
}
