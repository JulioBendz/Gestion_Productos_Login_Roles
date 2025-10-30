using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_Productos_Login_Roles.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public required string Nombre { get; set; }

        [Required, StringLength(50), Column("Usuario")]
        public required string UsuarioNombre { get; set; }

        [Required, StringLength(100)]
        public required string Clave { get; set; }

        [Required]
        public required string Rol { get; set; }
    }
}
