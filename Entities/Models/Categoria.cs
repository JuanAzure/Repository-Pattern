using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Categoria")]
    public class Categoria
    {
        [Key]
        
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "Nombre is requerid")]
        [StringLength(100, ErrorMessage = "Descripcion can`t be than 60 characters")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Descripcion is requerid")]
        [StringLength(100, ErrorMessage = "Descripcion can`t be than 60 characters")]
        public string Descripcion { get; set; }
        public bool? Condicion { get; set; }
        public ICollection<Articulo> Articulos { get; set; }
    }
}
