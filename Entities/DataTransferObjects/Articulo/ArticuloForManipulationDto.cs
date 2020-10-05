using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public abstract class ArticuloForManipulationDto
    {
        [Required(ErrorMessage = "Codigo is requerid")]
        [MinLength(5, ErrorMessage = "Codigo min = 5 characters")]
        [MaxLength(11, ErrorMessage = "Codigo max = 11 characters")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Nombre is requerid")]
        [MinLength(5, ErrorMessage = "Nombre min = 5 characters")]
        [MaxLength(60, ErrorMessage = "Nombre max = 60 characters")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "PrecioVenta is requerid")]
        [Range(0.5, 500, ErrorMessage = "PrecioVenta min = S/0.5, max = S/500.00")]

        public decimal PrecioVenta { get; set; }

        [Required(ErrorMessage = "Stock is requerid")]
        [Range(1, 100, ErrorMessage = "Stock min = 1, Stock = 100")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Descripcion is requerid")]
        [MinLength(5, ErrorMessage = "Descripcion min = 5 characters")]
        [MaxLength(60, ErrorMessage = "Descripcion max = 60 characters")]

        public string Descripcion { get; set; }
        public bool Condicion { get; set; }

    }
}
