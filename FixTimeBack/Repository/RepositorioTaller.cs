using FixTimeBack.Data;
using FixTimeBack.Interfaces;
using Microsoft.EntityFrameworkCore;
using TixTimeModels.Modelos;
using TixTimeModels.ModelosDTO;

namespace FixTimeBack.Repository
{
    public class RepositorioTaller : ITallerRepository
    {
        private readonly DataContext _context;
        public RepositorioTaller(DataContext context)
        {
            _context = context;
        }

        public async Task AddGarage(Taller taller)
        {
            await _context.Taller.AddAsync(taller);
        }

        public async Task<List<Taller>> GetAllGarages()
        {
            return await _context.Taller.ToListAsync();
        }

        public async Task<List<Taller>> GetGarageByAdmin(string userid)
        {
            return await _context.Taller.Where(e=> e.AdministradorID== userid).ToListAsync();

        }

        public async Task<Taller> GetGarageById(int id)
        {
            return await _context.Taller.FindAsync(id);
        }

        public async Task<List<Taller>> GetGarageByLocation(string location)
        {
            return await _context.Taller.Where(e => e.Ubicacion == location).ToListAsync();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public Task UpdateGarage(Taller taller)
        {
            _context.Entry(taller).State = EntityState.Modified;
            return Task.CompletedTask;
        }
    }
}
