using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TixTimeModels.ModelosDTO
{
    public class ContraseñaDTO
    {
        public string? UsuarioID { get; set; }
        public string? AntiguaContraseña { get; set; }
        public string? NuevaContraseña { get; set; }
    }
}
