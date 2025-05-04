using FixTimeBack.Data;
using FixTimeBack.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TixTimeModels.Modelos;
using TixTimeModels.ModelosDTO;

namespace FixTimeBack.Repository
{
    public class RepositorioServicios : IServicioRepository
    {

        private readonly DataContext _context;

        public RepositorioServicios(DataContext context)
        {
            _context = context;   
        }
        public async Task AddService(Servicio servicio)
        {
            await _context.Servicio.AddAsync(servicio);
        }

        public async Task<List<Servicio>> GetServiceByGarageId(int garageId)
        {
            return await _context.Servicio.Where(e=> e.TallerId==garageId).ToListAsync();
        }

        public async Task<Servicio> GetServiceById(int id)
        {
            return await _context.Servicio.FindAsync(id);
        }

        public async Task<List<Servicio>> GetServices()
        {
            return await _context.Servicio.ToListAsync();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public Task UpdateService(Servicio servicio)
        {
            _context.Entry(servicio).State = EntityState.Modified;
            return Task.CompletedTask;
        }
    }
}
