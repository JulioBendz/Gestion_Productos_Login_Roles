using System.ComponentModel.DataAnnotations;

namespace Gestion_Productos_Login_Roles.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public required string Nombre { get; set; }
    }
}
