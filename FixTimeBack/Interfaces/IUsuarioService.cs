using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Globalization;
using TixTimeModels.Modelos;
using TixTimeModels.ModelosDTO;

namespace FixTimeBack.Interfaces
{
    public interface IUsuarioService
    {
        Task<SesionDTO> LoginUser(LoginDTO loginDTO);
        Task<Usuario> SignUser(UsuarioDTO usuarioDTO);
        Task<Usuario> GetProfileUser(string UsuarioID);
        Task<Usuario> UpdateProfileUser(Usuario usuario,UsuarioDTO usuarioDTO);
        Task<Usuario> UpdatePasswordUser(Usuario usuario, ContraseñaDTO contraseñaDTO);
    }
}
