using Azure.Core;
using FixTimeBack.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using TixTimeModels.Modelos;
using TixTimeModels.ModelosDTO;

namespace FixTimeBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculoController : ControllerBase
    {
        private readonly IVehiculoService _vehiculoservices;
        public VehiculoController(IVehiculoService vehiculoservices)
        {
            _vehiculoservices = vehiculoservices;
        }


        [Authorize(Roles = "Cliente")]
        [HttpPost("AgregarVehiculo")]
        public async Task<ActionResult<Vehiculo>> AgregarVehiculo([FromBody] Vehiculo vehiculo)
        {
            if (vehiculo == null)
            {
                return BadRequest("Debes de llenar todos los campos");
            }

            var vehiculocreado = await _vehiculoservices.AgregarVehiculo(vehiculo);
            if (vehiculocreado.VehiculoID != 0)
            {
                return Ok(new { Messahe = "Vehiculo creado correctamente", Vehiculo = vehiculocreado });
            }

            return BadRequest("No se ha podido ingresar el vehiculo al sistema");
        }


        [Authorize(Roles = "Cliente")]
        [HttpPut("ActualizarVehiculo/{id}")]
        public async Task<ActionResult<Vehiculo>> ActualizarInfoVehiculo(int id, [FromBody] VehiculoDTO vehiculoDTO )
        {
            var vehiculo = await _vehiculoservices.ObtenerVehiculoPorId(id);
            if(vehiculo == null)
            {
                return NotFound("No se ha encontrado el vehiculo que buscas");
            }

            var updatecar = await _vehiculoservices.ActualizaInformacionVehiculo(vehiculo,vehiculoDTO);
            if (updatecar.VehiculoID != 0)
            {
                return Ok(new { Message = "Se ha actualizado correctamente la informacion del vehiculo", Car = updatecar });
            }

            return BadRequest("No se ha podido actualizar la informacion del vehiculo");
        }

        [Authorize(Roles = "Administrador,Cliente")]
        [HttpGet("ObtenerVehiculoPorClienteID/{UsuarioID}")]
        public async Task<ActionResult<IEnumerable<Vehiculo>>> ObtenerVehiculosPorUsuarioID(string UsuarioID)
        {
            var vehiculos = await _vehiculoservices.ObtenerVehiculosRegistradosClienteID(UsuarioID);
            if (vehiculos == null)
            {
                return BadRequest($"Aun no ha vehiculos registrados para el usuario {UsuarioID}");
            }

            return vehiculos;
        }

        [Authorize(Roles = "Cliente,Administrador")]
        [HttpGet("ObtenerVehiculoPorId/{id}")]
        public async Task<ActionResult<Vehiculo>> ObtenerVehiculoPoID(int id)
        {
            var vehiculo = await _vehiculoservices.ObtenerVehiculoPorId(id);
            if(vehiculo == null)
            {
                return NotFound("No se encuentra el vehiculo que estas buscando");
            }
            return vehiculo;
        }
    }
}
