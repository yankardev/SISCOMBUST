using System.ComponentModel.DataAnnotations;

namespace SISCOMBUST.Models
{
    public class Proveedor
    {
        [Key]
        public int IdProveedor { get; set; }

        [Required, StringLength(100)]
        public string NombreProveedor { get; set; }

        [StringLength(11)]
        public string RUC { get; set; }

        [StringLength(50)]
        public string Telefono { get; set; }

        [StringLength(100)]
        public string Correo { get; set; }
    }
}
