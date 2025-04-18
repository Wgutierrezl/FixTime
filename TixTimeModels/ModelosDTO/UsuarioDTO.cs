using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TixTimeModels.ModelosDTO
{
    public class UsuarioDTO
    {
        public string? UsuarioID { get; set; }
        public string? NombreCompleto { get; set; }
        public string? DocumentoID { get; set; }
        public string? CorreoElectronico { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public string? Contrasena { get; set; }
        public string? TipoUsuario { get; set; }
        public bool Estado { get; set; }
    }
}
