using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
   public class CategoriaForCreationDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Condicion { get; set; }
        public IEnumerable<ArticuloForCreationDto> Articulos { get; set; }

    }
}
