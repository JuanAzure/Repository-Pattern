using System.ComponentModel.DataAnnotations;
namespace Entities.DataTransferObjects
{
   public class CategoriaForManipulationDto
    {
        [Required(ErrorMessage = "Nombre is requerid")]
        [StringLength(60, ErrorMessage = "Maximum length for the Nombre is 60 characters.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Descripcion is requerid")]
        [StringLength(60, ErrorMessage = "Maximum length for the Descripcion is 60 characters.")]
        public string Descripcion { get; set; }
        public bool Condicion { get; set; }
    }
}
