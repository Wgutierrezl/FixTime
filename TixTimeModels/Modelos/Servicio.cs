using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TixTimeModels.Modelos
{
    public class Servicio
    {
        [Key]
        public int ServicioID { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int? TallerId { get; set; }
    }
}
