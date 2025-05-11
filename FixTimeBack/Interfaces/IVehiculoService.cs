using TixTimeModels.Modelos;
using TixTimeModels.ModelosDTO;

namespace FixTimeBack.Interfaces
{
    public interface IVehiculoService
    {
        Task<Vehiculo> AgregarVehiculo(VehiculoDTO vehiculoDTO);
        Task<Vehiculo> ActualizaInformacionVehiculo(Vehiculo vehiculo,VehiculoDTO vehiculoDTO);
        Task<List<Vehiculo>> ObtenerVehiculosRegistradosClienteID(string UsuarioID);
        Task<Vehiculo> ObtenerVehiculoPorId(int id);

    }
}
