using FixTimeBack.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using TixTimeModels.Modelos;
using TixTimeModels.ModelosDTO;

namespace FixTimeBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TallerController : ControllerBase
    {
        private readonly ITallerService _tallerservice;
        public TallerController(ITallerService tallerservices)
        {
            _tallerservice = tallerservices;
        }

        [Authorize(Roles ="Administrador")]
        [HttpPost("RegistrarTaller")]
        public async Task<ActionResult<Taller>> RegistrarTaller(TallerDTO taller)
        {
            if (taller == null)
            {
                return BadRequest("Debes de llenar todos los campos");
            }

            var tallercreado=await _tallerservice.AgregarTaller(taller);
            if (tallercreado.TallerID != 0)
            {
                return Ok(taller);
            }
            return BadRequest("No se ha podido agregar el taller a el sistema");
        }

        [Authorize(Roles ="Administrador")]
        [HttpPut("ActualizarInformacionTaller/{id}")]
        public async Task<ActionResult<Taller>> ActualizarTaller(int id, TallerDTO tallerDTO)
        {
            if (tallerDTO == null)
            {
                return BadRequest("Debes de llebar todos los campos");
            }

            var taller = await _tallerservice.ObtenerTaller(id);
            if(taller== null)
            {
                return NotFound("No se ha podido encontrado el taller que buscas");
            }

            var talleractualizado = await _tallerservice.ActualizarTaller(taller, tallerDTO);
            if (talleractualizado.TallerID != 0)
            {
                return Ok(talleractualizado);
            }

            return BadRequest("No se ha podido actualizar la informacion del taller");
        }

        [Authorize(Roles ="Administrador,Cliente")]
        [HttpGet("ObtenerTalleres")]
        public async Task<ActionResult<IEnumerable<Taller>>> ObtenerRegistroTalleres()
        {
            return await _tallerservice.ObtenerTalleres();
        }


        [Authorize(Roles ="Administrador")]
        [HttpGet("ObtenerTalleresPorAdministrador/{AdministradorID}")]
        public async Task<ActionResult<IEnumerable<Taller>>> ObtenerTalleresPorAdministradorID(string AdministradorID)
        {
            return await _tallerservice.ObtenerTalleresPorAdministrador(AdministradorID);
        }

        [Authorize(Roles ="Cliente")]
        [HttpGet("ObtenerTalleresPorUbicacion/{Ubicacion}")]
        public async Task<ActionResult<IEnumerable<Taller>>> ObtenerTalleresPorUbicacion(string Ubicacion)
        {
            return await _tallerservice.ObtenerTallerPorUbicacion(Ubicacion);
        }

        [Authorize(Roles ="Administrador,Cliente")]
        [HttpGet("ObtenerTallerPorID/{id}")]
        public async Task<ActionResult<Taller>> ObtenerTallerPorCodigo(int id)
        {
            var taller = await _tallerservice.ObtenerTaller(id);
            if(taller == null)
            {
                return NotFound("No se ha podido encontrar el taller que buscabas");
            }

            return Ok(taller);
        }
    }
}
