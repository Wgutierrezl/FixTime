using FixTimeBack.Custom;
using FixTimeBack.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TixTimeModels.Modelos;
using TixTimeModels.ModelosDTO;

namespace FixTimeBack.Interfaces
{
    public class RepositorioUsuarios : IServicioUsuarios
    {
        private readonly DataContext _context;
        private readonly Utilidades _utilidades;

        public RepositorioUsuarios(DataContext context,Utilidades utilidades)
        {
            _context = context;
            _utilidades = utilidades;
        }

        public async Task<Usuario> ActualizarContraseña(Usuario usuario, ContraseñaDTO contraseñaDTO)
        {
            if(usuario.Contrasena != _utilidades.EncryptSHA256(contraseñaDTO.AntiguaContraseña))
            {
                return null;
            }

            usuario.Contrasena = _utilidades.EncryptSHA256(contraseñaDTO.NuevaContraseña);
            _context.Entry(usuario).State=EntityState.Modified;
            await _context.SaveChangesAsync();
            return usuario;
           
        }


        public async Task<Usuario> ActualizarPerfil(UsuarioDTO usuarioDTO)
        {
            var usuario = await _context.Usuario.FindAsync(usuarioDTO.UsuarioID);
            if(usuario == null)
            {
                return null;
            }

            usuario.UsuarioID = usuarioDTO.UsuarioID;
            usuario.NombreCompleto = usuarioDTO.NombreCompleto;
            usuario.DocumentoID = usuarioDTO.DocumentoID;
            usuario.CorreoElectronico = usuarioDTO.CorreoElectronico;
            usuario.Telefono = usuarioDTO.Telefono;
            usuario.Direccion = usuarioDTO.Direccion;
            usuario.Estado = usuarioDTO.Estado;
            usuario.TipoUsuario = usuarioDTO.TipoUsuario;

            if (!string.IsNullOrWhiteSpace(usuarioDTO.Contrasena))
            {
                usuario.Contrasena = _utilidades.EncryptSHA256(usuarioDTO.Contrasena); 
            }

            _context.Entry(usuario).State=EntityState.Modified;
            await _context.SaveChangesAsync();
            return usuario;
        }


        public async Task<SesionDTO> Logearse(LoginDTO loginDTO)
        {
            var user= await _context.Usuario.Where(e => e.CorreoElectronico == loginDTO.Email && _utilidades.EncryptSHA256(loginDTO.Password!) == e.Contrasena).FirstOrDefaultAsync();
            if (user == null)
            {
                return null;

            }

            return new SesionDTO
            {
                UsuarioID=user.UsuarioID,
                Nombre = user.NombreCompleto,
                Correo=user.CorreoElectronico,
                Rol=user.TipoUsuario,
                Token=_utilidades.GenerateJWT(user)
            };
        }


        public async Task<Usuario> ObtenerPerfil(string UsuarioID)
        {
            return await _context.Usuario.FindAsync(UsuarioID);
        }


        public async Task<Usuario> Registrarse(UsuarioDTO usuarioDTO)
        {

            var user = new Usuario
            {
                UsuarioID = usuarioDTO.UsuarioID,
                NombreCompleto = usuarioDTO.NombreCompleto,
                DocumentoID = usuarioDTO.DocumentoID,
                CorreoElectronico = usuarioDTO.CorreoElectronico,
                Telefono = usuarioDTO.Telefono,
                Direccion = usuarioDTO.Direccion,
                Contrasena = _utilidades.EncryptSHA256(usuarioDTO.Contrasena),
                TipoUsuario = usuarioDTO.TipoUsuario,
                Estado = usuarioDTO.Estado,
                
            };
           

            _context.Usuario.Add(user);
            await _context.SaveChangesAsync();
            return user;
            
        }
    }
}
