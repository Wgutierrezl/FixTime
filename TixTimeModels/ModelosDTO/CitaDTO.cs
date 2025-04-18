using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TixTimeModels.ModelosDTO
{
    public class CitaDTO
    {
        public string? ClienteId { get; set; }
        public int? TallerId { get; set; }
        public int? ServicioId { get; set; }
        public int? VehiculoId { get; set; }
        public DateTime FechaHora { get; set; }
        public string? Estado { get; set; }
        public string? RecepcionistaId { get; set; }
    }
}
