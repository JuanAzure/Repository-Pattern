using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class ArticuloForUpdateDto : ArticuloForManipulationDto
    {        
        [Range(1, 50, ErrorMessage = "Categoria requerid")]
        public int CategoriaId { get; set; }
    }
}
