using Azure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SISCOMBUST.Models
{
    public class Unidad
    {
        [Key]
        public int IdUnidad { get; set; }

        [Required, StringLength(20)]
        public string Placa { get; set; }

        [StringLength(50)]
        public string TipoUnidad { get; set; } // Ejemplo: Cisterna, Camión, Camioneta

        // Relación con Operacion
        public int IdOperacion { get; set; }
        [ForeignKey("IdOperacion")]
        public Operacion Operacion { get; set; }
    }
}
