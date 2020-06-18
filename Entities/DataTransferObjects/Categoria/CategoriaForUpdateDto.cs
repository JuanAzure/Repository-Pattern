using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
   public class CategoriaForUpdateDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Condicion { get; set; }
        //public ICollection<Articulo> articulos { get; set; }
    }
}
