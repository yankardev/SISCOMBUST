using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SISCOMBUST.Models
{
    public class SolicitudCompra
    {
        [Key]
        public int IdSolicitud { get; set; }

        [Required(ErrorMessage = "La fecha de solicitud es obligatoria")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Solicitud")]
        public DateTime FechaSolicitud { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un proveedor")]
        [Display(Name = "Proveedor")]
        public int IdProveedor { get; set; }

        [ForeignKey("IdProveedor")]
        public Proveedor? Proveedor { get; set; }

        [Required(ErrorMessage = "Debe ingresar la cantidad solicitada")]
        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Galones Solicitados")]
        public decimal GalonesSolicitados { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Precio Referencial por Galón")]
        public decimal? PrecioReferencial { get; set; }

        [NotMapped]
        [Display(Name = "Monto Total Estimado")]
        public decimal MontoEstimado => (GalonesSolicitados * (PrecioReferencial ?? 0));

        [Required]
        [StringLength(20)]
        [Display(Name = "Estado")]
        public string Estado { get; set; } = "Pendiente";

        [StringLength(100)]
        public string? Solicitante { get; set; }

        [StringLength(200)]
        public string? Observacion { get; set; }
    }
}
