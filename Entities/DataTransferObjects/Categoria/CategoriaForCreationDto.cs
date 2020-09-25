using System.Collections.Generic;
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
