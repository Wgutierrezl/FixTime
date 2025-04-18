using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TixTimeModels.Modelos
{
    public class Taller
    {
        [Key]
        public int TallerID { get; set; }
        public string? Nombre { get; set; }
        public string? Ubicacion { get; set; }
        public string? HorarioAtencion { get; set; }
        public string? AdministradorID { get; set; }
    }
}
