using FixTimeBack.Data;
using FixTimeBack.Interfaces;
using Microsoft.EntityFrameworkCore;
using TixTimeModels.Modelos;
using TixTimeModels.ModelosDTO;

namespace FixTimeBack.Repository
{
    public class RepositorioVehiculo : IVehiculoService
    {
        private readonly DataContext _context;
        public RepositorioVehiculo(DataContext context)
        {
            _context = context;
        }
        public async Task<Vehiculo> ActualizaInformacionVehiculo(Vehiculo vehiculo, VehiculoDTO vehiculoDTO)
        {
            vehiculo.Marca = vehiculoDTO.Marca;
            vehiculo.Modelo = vehiculoDTO.Modelo;
            vehiculo.Año = vehiculoDTO.Año;
            vehiculo.ProblemaDescripcion = vehiculoDTO.ProblemaDescripcion;

            if (!string.IsNullOrWhiteSpace(vehiculoDTO.ClienteID))
            {
                vehiculo.ClienteID = vehiculoDTO.ClienteID;
            }

            _context.Entry(vehiculo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return vehiculo;
        }

        public async Task<Vehiculo> AgregarVehiculo(Vehiculo vehiculo)
        {
            _context.Vehiculo.Add(vehiculo);
            await _context.SaveChangesAsync();
            return vehiculo;
        }

        public async Task<List<Vehiculo>> ObtenerVehiculosRegistradosClienteID(string UsuarioID)
        {
            return await _context.Vehiculo.Where(e => e.ClienteID.ToLower() == UsuarioID.ToLower()).ToListAsync();
        }

        public async Task<Vehiculo> ObtenerVehiculoPorId(int id)
        {
            return await _context.Vehiculo.FindAsync(id);
        }
    }
}
