using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SISCOMBUST.Models
{
    public class Compra
    {
        [Key]
        public int IdCompra { get; set; }

        [Required]
        public DateTime FechaCompra { get; set; }

        [Precision(10, 2)]
        public decimal GalonesComprados { get; set; }

        [Precision(10, 3)]
        public decimal PrecioPorGalon { get; set; }

        [NotMapped]
        public decimal Total => GalonesComprados * PrecioPorGalon;


        // Relación con Proveedor
        public int IdProveedor { get; set; }
        [ForeignKey("IdProveedor")]
        public Proveedor Proveedor { get; set; }
    }
}
