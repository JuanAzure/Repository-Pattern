using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.Venta.DetalleVenta
{
   public class DetalleVentaForCreation
    {                        
        public int ArticuloId { get; set; }        
        public int Cantidad { get; set; }        
        public decimal PrecioVenta { get; set; }        
        public decimal Descuento { get; set; }        
    }
}
