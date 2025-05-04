using FixTimeBack.Interfaces;
using Microsoft.VisualBasic;
using TixTimeModels.Modelos;
using TixTimeModels.ModelosDTO;

namespace FixTimeBack.Service
{
    public class VehiculoService : IVehiculoService
    {
        private readonly IVehiculoRepository _repo;

        public VehiculoService(IVehiculoRepository repo)
        {
            _repo = repo;
        }
        public async Task<Vehiculo> ActualizaInformacionVehiculo(Vehiculo vehiculo, VehiculoDTO vehiculoDTO)
        {
            vehiculo.ClienteID = string.IsNullOrWhiteSpace(vehiculoDTO.ClienteID) ? vehiculo.ClienteID : vehiculoDTO.ClienteID;
            vehiculo.ProblemaDescripcion = vehiculoDTO.ProblemaDescripcion;
            vehiculo.Año = vehiculoDTO.Año;
            vehiculo.Marca = vehiculoDTO.Marca;
            vehiculo.Modelo = vehiculoDTO.Modelo;

            await _repo.UpdateVehicule(vehiculo);
            await _repo.SaveChanges();

            return vehiculo;
        }

        public async Task<Vehiculo> AgregarVehiculo(Vehiculo vehiculo)
        {
            await _repo.AddVehicule(vehiculo);
            await _repo.SaveChanges();

            return vehiculo;
        }

        public async Task<Vehiculo> ObtenerVehiculoPorId(int id)
        {
            return await _repo.GetVehiculeById(id);
        }

        public async Task<List<Vehiculo>> ObtenerVehiculosRegistradosClienteID(string UsuarioID)
        {
            return await _repo.GetVehiculeByUserId(UsuarioID);
        }
    }
}
