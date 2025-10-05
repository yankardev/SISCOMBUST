using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SISCOMBUST.Models
{
    public class SolicitudCompra
    {
        [Key]
        public int IdSolicitud { get; set; }

        [Required]
        public DateTime FechaSolicitud { get; set; }

        [Precision(10, 2)]
        public decimal GalonesSolicitados { get; set; }


        [Required, StringLength(20)]
        public string Estado { get; set; } = "Pendiente"; // Pendiente, Aprobado, Rechazado

        // Relación con Usuario (quién la solicita)
        public int IdUsuario { get; set; }
        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }
    }
}
