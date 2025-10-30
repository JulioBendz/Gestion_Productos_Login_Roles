using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_Productos_Login_Roles.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Required]
        public required string Nombre { get; set; }

        [Range(0.01, 9999.99, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal Precio { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo")]
        public int Stock { get; set; }

        [Display(Name = "Categoría")]
        [Range(1, int.MaxValue, ErrorMessage = "Seleccione una categoría")]
        public int IdCategoria { get; set; }

        [ForeignKey("IdCategoria")]
        public Categoria? Categoria { get; set; }
    }
}
