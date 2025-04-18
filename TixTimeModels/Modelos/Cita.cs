using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TixTimeModels.Modelos
{
    public class Cita
    {
        [Key]
        public int CitaId { get; set; } 
        public string? ClienteId { get; set; }
        public int? TallerId { get; set; }
        public int? ServicioId { get; set; }
        public int? VehiculoId { get; set; }
        public DateTime FechaHora { get; set; }
        public string? Estado { get;set; }
        public string? RecepcionistaId { get; set; }
    }
}
