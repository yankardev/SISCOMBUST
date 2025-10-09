using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SISCOMBUST.Models
{
    public class Compra
    {
        [Key]
        public int IdCompra { get; set; }

        [Required(ErrorMessage = "La fecha de compra es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime FechaCompra { get; set; }

        [Required]
        [Display(Name = "Proveedor")]
        public int IdProveedor { get; set; }
       
        [ForeignKey("IdProveedor")]
        public Proveedor? Proveedor { get; set; }

        [Required]
        [Display(Name = "Galones comprados")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal GalonesComprados { get; set; }

        [Required]
        [Display(Name = "Precio por galón (S/.)")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal PrecioPorGalon { get; set; }

        [Display(Name = "Monto total (S/.)")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal MontoTotal => GalonesComprados * PrecioPorGalon;

        [StringLength(20)]
        [Display(Name = "N° de factura")]
        public string? NumeroFactura { get; set; }

        [StringLength(200)]
        public string? Observacion { get; set; }

        [Display(Name = "Solicitud de Compra")]
        public int? IdSolicitud { get; set; }

        [ForeignKey("IdSolicitud")]
        public SolicitudCompra? SolicitudCompra { get; set; }

    }
}
