using System.ComponentModel.DataAnnotations;

namespace SISCOMBUST.Models
{
    public class Operacion
    {
        [Key]
        public int IdOperacion { get; set; }

        [Required, StringLength(100)]
        public string NombreOperacion { get; set; }

        [StringLength(150)]
        public string Descripcion { get; set; }
    }
}
