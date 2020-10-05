using System.Collections.Generic;
namespace Entities.DataTransferObjects
{
   public class CategoriaForCreationDto : CategoriaForManipulationDto
    {       
      public IEnumerable<ArticuloForCreationDto> Articulos { get; set; }
    }
}
