﻿using Azure.Core;
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
        private readonly IServicioUsuarios UserService;
        public UsuariosController(IServicioUsuarios userservice)
        {
            UserService = userservice;
        }

        [AllowAnonymous]
        [HttpPost("/Logearse")]
        public async Task<IActionResult> Logearse(LoginDTO loginDTO)
        {
            var user=await UserService.Logearse(loginDTO);
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
            var user = await UserService.ObtenerPerfil(UsuarioID);
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

            var updateUser = await UserService.ActualizarPerfil(usuarioDTO);
            if(updateUser == null)
            {
                return NotFound("No se ha podido encontrar al usuario que quieres modificar");
            }
            return Ok(updateUser);
        }

        [AllowAnonymous]
        [HttpPost("/Registrarse")]
        public async Task<ActionResult<Usuario>> Registrarse(UsuarioDTO usuario)
        {
            var user=await UserService.Registrarse(usuario);
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
            var user=await UserService.ObtenerPerfil(id);
            if (user == null)
            {
                return NotFound("No se ha encontrado el usuario");
            }

            var userupdate = await UserService.ActualizarContraseña(user, contraseñaDTO);
            if(userupdate == null)
            {
                return BadRequest("Contraseñas actuales no coinciden");
            }
            return Ok(userupdate);
        }

    }
}
