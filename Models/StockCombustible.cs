using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SISCOMBUST.Models
{
    public class StockCombustible
    {
        [Key]
        public int IdStock { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Galones Disponibles")]
        public decimal GalonesDisponibles { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Última Actualización")]
        public DateTime FechaActualizacion { get; set; } = DateTime.Now;
    }
}
