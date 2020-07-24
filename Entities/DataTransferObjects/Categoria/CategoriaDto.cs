using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class CategoriaDto
    {        
        public int categoriaId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Condicion { get; set; }
        public ICollection<ArticuloDto> Articulos { get; set; }
    }
}
