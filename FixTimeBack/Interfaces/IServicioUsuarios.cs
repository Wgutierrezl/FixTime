using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Globalization;
using TixTimeModels.Modelos;
using TixTimeModels.ModelosDTO;

namespace FixTimeBack.Interfaces
{
    public interface IServicioUsuarios
    {
        Task<SesionDTO> Logearse(LoginDTO loginDTO);
        Task<Usuario> Registrarse(UsuarioDTO usuarioDTO);
        Task<Usuario> ObtenerPerfil(string UsuarioID);
        Task<Usuario> ActualizarPerfil(UsuarioDTO usuarioDTO);
        Task<Usuario> ActualizarContraseña(Usuario usuario, ContraseñaDTO contraseñaDTO);
    }
}
