using FixTimeBack.Data;
using FixTimeBack.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Reflection.Metadata.Ecma335;
using TixTimeModels.Modelos;
using TixTimeModels.ModelosDTO;

namespace FixTimeBack.Repository
{
    public class RepositorioCitas : ICitasRepository
    {
        private readonly DataContext _context;

        public RepositorioCitas(DataContext context)
        {
            _context = context;
        }
        public async Task AddAppointment(Cita cita)
        {
            await _context.Cita.AddAsync(cita);
        }

        public async Task<List<Cita>> GetAppointmenByServiceId(int serviceId)
        {
            return await _context.Cita.Where(e => e.ServicioId == serviceId).ToListAsync();
        }

        public async Task<List<Cita>> GetAppointmentByGarageId(int garageId)
        {
            return await _context.Cita.Where(e=>e.TallerId==garageId).ToListAsync();
        }

        public async Task<Cita> GetAppointmentById(int id)
        {
            return await _context.Cita.FindAsync(id);
        }

        public async Task<List<Cita>> GetAppointmentByRecepcionistId(string recepcionistid)
        {
            return await _context.Cita.Where(e => e.RecepcionistaId.ToLower() == recepcionistid.ToLower()).ToListAsync();
        }

        public async Task<List<Cita>> GetAppointmentByUserId(string userId)
        {
            return await _context.Cita.Where(e => e.ClienteId.ToLower() == userId.ToLower()).ToListAsync();

        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public Task UpdateAppoinment(Cita cita)
        {
            _context.Entry(cita).State = EntityState.Modified;
            return Task.CompletedTask;
        }
    }
}
