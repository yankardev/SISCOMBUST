using System.ComponentModel.DataAnnotations;

namespace SISCOMBUST.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        [Required, StringLength(100)]
        public string Nombre { get; set; }

        [Required, StringLength(50)]
        public string UsuarioLogin { get; set; }

        [Required, StringLength(50)]
        public string Clave { get; set; }

        [Required, StringLength(30)]
        public string Rol { get; set; } // Supervisor, Logistica, Gerencia
    }
}
