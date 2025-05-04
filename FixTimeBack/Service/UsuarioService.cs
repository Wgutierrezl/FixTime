using FixTimeBack.Custom;
using FixTimeBack.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using TixTimeModels.Modelos;
using TixTimeModels.ModelosDTO;

namespace FixTimeBack.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repo;
        private readonly ITokenService _token;
        private readonly IPasswordHasher _passwordHasher;

        public UsuarioService(IUsuarioRepository repo,ITokenService token,IPasswordHasher password)
        {
            _repo = repo;
            _token=token;
            _passwordHasher=password;
        }
        public async Task<Usuario> GetProfileUser(string UsuarioID)
        {
            return await _repo.GetProfile(UsuarioID);
        }

        public async Task<SesionDTO> LoginUser(LoginDTO loginDTO)
        {
            var useremail = await _repo.GetEmailUser(loginDTO.Email);
            if (useremail == null)
            {
                return null;
            }

            if (useremail.Contrasena != _passwordHasher.Encryptsha256(loginDTO.Password))
            {
                throw new Exception("Correo o contraseña incorrectas");
            }

            return new SesionDTO
            {
                UsuarioID = useremail.UsuarioID,
                Nombre = useremail.NombreCompleto,
                Correo = useremail.CorreoElectronico,
                Rol = useremail.TipoUsuario,
                Token = _token.GenerateJWT(useremail)

            };
        }

        public async Task<Usuario> SignUser(UsuarioDTO usuarioDTO)
        {
            var user = new Usuario
            {
                UsuarioID = usuarioDTO.UsuarioID,
                NombreCompleto = usuarioDTO.NombreCompleto,
                DocumentoID = usuarioDTO.DocumentoID,
                CorreoElectronico = usuarioDTO.CorreoElectronico,
                Telefono = usuarioDTO.Telefono,
                Direccion = usuarioDTO.Direccion,
                Contrasena = _passwordHasher.Encryptsha256(usuarioDTO.Contrasena),
                TipoUsuario = usuarioDTO.TipoUsuario,
                Estado = usuarioDTO.Estado

            };

            await _repo.SignUser(user);
            await _repo.SaveChanges();

            return user;
        }

        public async Task<Usuario> UpdatePasswordUser(Usuario usuario, ContraseñaDTO contraseñaDTO)
        {
            if (usuario.Contrasena != _passwordHasher.Encryptsha256(contraseñaDTO.AntiguaContraseña))
            {
                throw new Exception("Las contraseñas actuales no coinciden");
            }

            usuario.Contrasena = _passwordHasher.Encryptsha256(contraseñaDTO.NuevaContraseña);

            await _repo.UpdateUser(usuario);
            await _repo.SaveChanges();

            return usuario;
        }

        public async Task<Usuario> UpdateProfileUser(Usuario usuario, UsuarioDTO usuarioDTO)
        {
            usuario.NombreCompleto = usuarioDTO.NombreCompleto;

            if (!string.IsNullOrWhiteSpace(usuarioDTO.DocumentoID))
            {
                usuario.DocumentoID=usuarioDTO.DocumentoID;
            }

            usuario.CorreoElectronico = usuarioDTO.CorreoElectronico;
            usuario.Telefono = usuarioDTO.Telefono;
            usuario.Direccion = usuarioDTO.Direccion;
            usuario.TipoUsuario = string.IsNullOrWhiteSpace(usuarioDTO.TipoUsuario) ? usuario.TipoUsuario : usuarioDTO.TipoUsuario;

            if (!string.IsNullOrEmpty(usuarioDTO.Contrasena))
            {
                usuario.Contrasena = _passwordHasher.Encryptsha256(usuarioDTO.Contrasena);
            }

            usuario.Estado = usuarioDTO.Estado;

            await _repo.UpdateUser(usuario);
            await _repo.SaveChanges();

            return usuario;
            

        }
    }
}
