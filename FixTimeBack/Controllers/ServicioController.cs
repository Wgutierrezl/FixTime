using FixTimeBack.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.VisualBasic;
using System.Text;
using TixTimeModels.Modelos;
using TixTimeModels.ModelosDTO;

namespace FixTimeBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicioController : ControllerBase
    {
        private readonly IServicioService _services;

        public ServicioController(IServicioService services)
        {
            _services=services;
        }

        [Authorize(Roles ="Administrador")]
        [HttpPost("AgregarServicio")]
        public async Task<ActionResult<Servicio>> AgregarServicio([FromBody] Servicio servicio)
        {
            if(servicio == null)
            {
                return BadRequest("Debes de completar todos los campos");
            }

            var servicecreated = await _services.AgregarServicios(servicio);
            if (servicio.ServicioID != 0)
            {
                return Ok(new { Message = "Servicio creado correctamene", Service = servicecreated });
            }

            return BadRequest("No se ha podido crear el servicio");
        }

        [Authorize(Roles ="Administrador")]
        [HttpPut("ActualizarServicio/{id}")]
        public async Task<ActionResult<Servicio>> ActualizarServicio(int id, [FromBody] ServiciosDTO serviciosDTO)
        {
            var service = await _services.ObtenerServicioPorId(id);
            if(service == null)
            {
                return NotFound("No se ha encontrado el servicio que buscas");
            }

            var updateservice = await _services.ActualizarServicio(service, serviciosDTO);
            if (updateservice.ServicioID != 0)
            {
                return Ok(new { Message = "Servicio actualizado correctamente" , Servicio = updateservice });
            }

            return BadRequest("No se ha podido crear el servicio");
        }


        [Authorize(Roles ="Cliente,Administrador")]
        [HttpGet("ObtenerServiciosPorTallerID({tallerid}")]
        public async Task<ActionResult<IEnumerable<Servicio>>> ObtenerServicioPorTallerId(int tallerid)
        {
            var servicios = await _services.ObtenerServiciosPorTallerId(tallerid);
            if(servicios== null)
            {
                return BadRequest("No hay servicios asignados a este taller");
            }
            return servicios;
        }


        [Authorize(Roles ="Administrador,Cliente")]
        [HttpGet("ObtenerServiciosPorId/{id}")]
        public async Task<ActionResult<Servicio>> ObtenerServiciosPorId(int id)
        {
            var servicio = await _services.ObtenerServicioPorId(id);
            if(servicio== null)
            {
                return NotFound("No se ha podido encontrar el servicio que buscabas");
            }

            return servicio;
        }
    }
}
