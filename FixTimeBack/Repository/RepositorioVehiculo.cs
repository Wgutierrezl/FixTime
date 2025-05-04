using FixTimeBack.Data;
using FixTimeBack.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using TixTimeModels.Modelos;
using TixTimeModels.ModelosDTO;

namespace FixTimeBack.Repository
{
    public class RepositorioVehiculo : IVehiculoRepository
    {
        private readonly DataContext _context;
        public RepositorioVehiculo(DataContext context)
        {
            _context = context;
        }

        public async Task AddVehicule(Vehiculo vehiculo)
        {
            await _context.Vehiculo.AddAsync(vehiculo);
        }

        public async Task<Vehiculo> GetVehiculeById(int id)
        {
            return await _context.Vehiculo.FindAsync(id);
        }

        public async Task<List<Vehiculo>> GetVehiculeByUserId(string userId)
        {
            return await _context.Vehiculo.Where(e => e.ClienteID.ToLower() == userId.ToLower()).ToListAsync();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public Task UpdateVehicule(Vehiculo vehiculo)
        {
            _context.Entry(vehiculo).State = EntityState.Modified;
            return Task.CompletedTask;
        }
    }
}
