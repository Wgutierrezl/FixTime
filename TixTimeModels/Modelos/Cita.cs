using System.ComponentModel.DataAnnotations;

namespace TixTimeModels.Modelos
{
    public class Cita
    {
        [Key] public int CitaId { get; set; }

        public string? ClienteId { get; set; }

        public int? TallerId { get; set; }
        public Taller? Taller { get; set; } // Relación

        public int? ServicioId { get; set; }
        public Servicio? Servicio { get; set; } // Relación

        public int? VehiculoId { get; set; }
        public Vehiculo? Vehiculo { get; set; } // Relación

        public DateTime FechaHora { get; set; }

        public string? Estado { get; set; }

        public string? RecepcionistaId { get; set; }
    }
}