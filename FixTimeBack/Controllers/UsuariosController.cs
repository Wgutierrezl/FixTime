using Azure.Core;
using FixTimeBack.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TixTimeModels.Modelos;
using TixTimeModels.ModelosDTO;

namespace FixTimeBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService UserService;
        public UsuariosController(IUsuarioService userservice)
        {
            UserService = userservice;
        }

        [AllowAnonymous]
        [HttpPost("/Logearse")]
        public async Task<IActionResult> Logearse(LoginDTO loginDTO)
        {
            var user = await UserService.LoginUser(loginDTO);
            if(user == null)
            {
                return BadRequest("correo o contraseñas incorrectas");
            }

            return Ok(user);
        }

        [Authorize(Roles ="Administrador,Cliente,Recepcionista")]
        [HttpGet("/ObtenerPerfil/{UsuarioID}")]
        public async Task<ActionResult<Usuario>> ObtenerPerfil(string UsuarioID)
        {
            var user = await UserService.GetProfileUser(UsuarioID);
            if( user == null)
            {
                return NotFound("No se ha podido encontrar al usuario");
            }
            return Ok(user);

        }
        [Authorize(Roles ="Administrador,Cliente,Recepcionista")]
        [HttpPut("/ActualizarPerfil/{UsuarioID}")]
        public async Task<ActionResult<Usuario>> ActualizarPerfil(string UsuarioID, UsuarioDTO usuarioDTO)
        {
            if(UsuarioID != usuarioDTO.UsuarioID)
            {
                return BadRequest("No coincide el id del parametro con el que quieres actualizar");
            }

            var updateUser = await UserService.GetProfileUser(UsuarioID);
            if(updateUser == null)
            {
                return NotFound("No se ha podido encontrar al usuario que quieres modificar");
            }

            var usermodified = await UserService.UpdateProfileUser(updateUser, usuarioDTO);
            if (usermodified == null)
            {
                return BadRequest("No se ha podido modificar la informacion del cliente");
            }

            return usermodified;
            }

        [AllowAnonymous]
        [HttpPost("/Registrarse")]
        public async Task<ActionResult<Usuario>> Registrarse(UsuarioDTO usuario)
        {
            var user = await UserService.SignUser(usuario);
            if(user == null)
            {
                return BadRequest("No se ha podido registrar al usuario");
            }
            return Ok(user);    
        }


        [Authorize(Roles ="Administrador,Cliente,Recepcionista")]
        [HttpPut("/ActualizarPassword/{id}")]
        public async Task<ActionResult<Usuario>> ActualizarContraseña(string id,ContraseñaDTO contraseñaDTO)
        {
            var user = await UserService.GetProfileUser(id);
            if (user == null)
            {
                return NotFound("No se ha encontrado el usuario");
            }

            var userupdate = await UserService.UpdatePasswordUser(user, contraseñaDTO);
            if(userupdate == null)
            {
                return BadRequest("Contraseñas actuales no coinciden");
            }
            return Ok(userupdate);
        }

        [AllowAnonymous]
        [HttpPost("RecuperarContraseña")]
        public async Task<IActionResult> RecuperarMiContraseña([FromBody] ForgotPasswordRequest email)
        {
            if(email == null)
            {
                return BadRequest("Debes de ingresar un correo valido");
            }

            var user=await UserService.ForgotMyPassword(email);
            if(user == null)
            {
                return BadRequest("El correo que ingresaste no existe");
            }

            return Ok(new { Mesage = "Token para recuperar contraseña", Sesion = user });
        }

        [AllowAnonymous]
        [HttpPut("IngresarNuevaContraseña")]
        public async Task<IActionResult> IngresarNuevaContraseña([FromBody] ResetPasswordRequest resetPasswordRequest)
        {
           
            var user_find=await UserService.ResetMyPassword(resetPasswordRequest);

            if (user_find == null)
            {
                return BadRequest("Tu token ha expirado");
            }

            return Ok(user_find);
        }

    }
}
