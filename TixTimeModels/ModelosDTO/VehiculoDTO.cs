using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TixTimeModels.ModelosDTO
{
    public class VehiculoDTO
    {
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public int Año { get; set; }
        public string? ProblemaDescripcion { get; set; }
        public string? ClienteID { get; set; }
    }
}
