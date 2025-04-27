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
    public class CitasController : ControllerBase
    {
        private ICitasService _servicecitas;

        public CitasController(ICitasService servicecitas)
        {
            _servicecitas=servicecitas;
        }


        [Authorize(Roles ="Cliente")]
        [HttpPost("AgregarNuevaCita")]
        public async Task<ActionResult<Cita>> AgregarUnaCita([FromBody] Cita cita)
        {
            if (cita == null)
            {
                return BadRequest("Debes de completar todos los campos");
            }

            var citacreada=await _servicecitas.AgregarCita(cita);
            if (citacreada.CitaId != 0)
            {
                return Ok(new { Message = "Cita agregada correctamente", Cita = citacreada });
            }

            return BadRequest("No se ha podido crear la cita");
        }


        [Authorize(Roles ="Cliente,Administrador")]
        [HttpPut("ActualizarCita/{id}")]
        public async Task<ActionResult<Cita>> ActualizarInformacionCita(int id, CitaDTO citaDTO)
        {
            var cita=await _servicecitas.ConsultarCitaPorId(id);
            if(cita == null)
            {
                return NotFound("No se ha podido encontrar la cita que buscas");
            }

            var updateappointment = await _servicecitas.ActualizarCita(cita, citaDTO);
            if(updateappointment.CitaId != 0)
            {
                return Ok(new { Message = "Cita actualizada correctamente", Cita = updateappointment });
            }
            return BadRequest("No se ha podido actualizar la cita");
        }

        [Authorize(Roles ="Recepcionista")]
        [HttpPut("ActualizarEstadoCita/{id}")]
        public async Task<ActionResult<Cita>> ActualizarEstadoCita(int id, [FromBody] EstadoDTO estadoDTO)
        {
            var cita = await _servicecitas.ConsultarCitaPorId(id);
            if (cita == null)
            {
                return NotFound("No se ha encontrado la cita que buscas");
            }

            var CitaEstadoActualizada=await _servicecitas.ActualizarEstadoCita(cita, estadoDTO);
            if (CitaEstadoActualizada.CitaId != 0)
            {
                return Ok(new {Message="Estado de cita actualizado correctamente",Cita=CitaEstadoActualizada});
            }

            return BadRequest("No se ha podido actualizar el estado de la cita");
        }

        [Authorize(Roles ="Administrador,Recepcionista")]
        [HttpGet("ConsultarCitasPorRcepecionistaID/{recepcionistaID}")]
        public async Task<ActionResult<IEnumerable<Cita>>> ConsultarCitasPorRecepcionista(string recepcionistaID)
        {
            var citas = await _servicecitas.ConsultarCitasPorRecepcionistaID(recepcionistaID);
            if (citas == null)
            {
                return BadRequest("El recepcionista no tiene citas asignadas");
            }

            return citas;
        }

        [Authorize(Roles ="Administrador,Cliente")]
        [HttpGet("ConsultarCitasPorClienteID/{clienteID}")]
        public async Task<ActionResult<IEnumerable<Cita>>> ConsultarCitasPorCliente(string clienteID)
        {
            var citas = await _servicecitas.ConsultarCitasPorClienteID(clienteID);
            if (citas == null)
            {
                return BadRequest("El cliente no tiene citas asignadas");
            }

            return citas;
        }

        [Authorize(Roles ="Administrador,Cliente,Recepcionista")]
        [HttpGet("ConsultarCitaPorID/{id}")]
        public async Task<ActionResult<Cita>> ConsultarCitaPorId(int id)
        {
            var cita = await _servicecitas.ConsultarCitaPorId(id);
            if (cita == null)
            {
                return NotFound("No se ha encontrado la cita que buscas");
            }
            return cita;
        }
    }
}
