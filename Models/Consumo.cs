using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SISCOMBUST.Models
{
    public class Consumo
    {
        [Key]
        public int IdConsumo { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "Debe ingresar la operación")]
        [StringLength(50)]
        public string Operacion { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe ingresar la ruta")]
        [StringLength(100)]
        public string Ruta { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe ingresar la cantidad de unidades")]
        [Range(0, 1000, ErrorMessage = "Las unidades deben ser un valor positivo")]
        public int Unidades { get; set; }

        [Required(ErrorMessage = "Debe ingresar los galones por unidad")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal GalonesPorUnidad { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Galones Consumidos")]
        public decimal GalonesConsumidos { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de salida")]
        public DateTime? FechaSalida { get; set; }

        [StringLength(100)]
        public string? Supervisor { get; set; }

        [StringLength(200)]
        public string? Observacion { get; set; }

        // Cálculo automático antes de guardar
        public void CalcularTotal()
        {
            GalonesConsumidos = Unidades * GalonesPorUnidad;
        }
    }
}
