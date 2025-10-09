using System.ComponentModel.DataAnnotations;

namespace SISCOMBUST.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        [StringLength(100)]
        public string NombreCompleto { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string NombreUsuario { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Password { get; set; } = string.Empty;

        [StringLength(20)]
        public string Rol { get; set; } = "Usuario"; // Por defecto


    }
}
