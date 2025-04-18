using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TixTimeModels.Modelos
{
    public class Vehiculo
    {
        [Key]
        public int VehiculoID { get; set; }
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public int Año { get; set; }
        public string? ProblemaDescripcion { get; set; }
        public string? ClienteID { get; set; }
    }
}
