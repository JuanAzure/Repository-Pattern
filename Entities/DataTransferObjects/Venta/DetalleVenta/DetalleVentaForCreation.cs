using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.Venta.DetalleVenta
{
   public class DetalleVentaForCreation
    {
                
        public int VentaId { get; set; }    
        public int ArticuloId { get; set; }        
        public int Cantidad { get; set; }        
        public decimal Precio { get; set; }        
        public decimal Descuento { get; set; }        
    }
}
