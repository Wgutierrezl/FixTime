using FixTimeBack.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using TixTimeModels.Modelos;
using TixTimeModels.ModelosDTO;

namespace FixTimeBack.Interfaces
{
    public class RepositorioCitas : IServiciosCitas
    {
        private readonly DataContext _context;

        public RepositorioCitas(DataContext context)
        {
            _context = context; 
            
        }
        public async Task<Cita> ActualizarCita(Cita cita, CitaDTO citaDTO)
        {
            if (!string.IsNullOrWhiteSpace(citaDTO.ClienteId))
            {
                cita.ClienteId = citaDTO.ClienteId;
            }

            cita.TallerId = citaDTO.TallerId ?? null;
            cita.ServicioId = citaDTO.ServicioId ?? null;
            cita.VehiculoId = citaDTO.VehiculoId ?? null;
            cita.FechaHora = citaDTO.FechaHora;
            cita.Estado = citaDTO.Estado;

            if (!string.IsNullOrWhiteSpace(citaDTO.RecepcionistaId))
            {
                cita.RecepcionistaId = citaDTO.RecepcionistaId;
            }
         
            _context.Entry(cita).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return cita;


        }

        public async Task<Cita> ActualizarEstadoCita(Cita cita, EstadoDTO estado)
        {
            cita.Estado = estado.Estado;
            _context.Entry(cita).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return cita;
        }

        public async Task<Cita> AgregarCita(Cita cita)
        {
            _context.Cita.Add(cita);
            await _context.SaveChangesAsync();
            return cita;
        }

        public async Task<Cita> ConsultarCitaPorId(int citaID)
        {
            return await _context.Cita.FindAsync(citaID);
        }

        public async Task<List<Cita>> ConsultarCitasPorClienteID(string UsuarioID)
        {
            return await _context.Cita.Where(e=> e.ClienteId.ToLower()==UsuarioID.ToLower()).ToListAsync();
        }

        public async Task<List<Cita>> ConsultarCitasPorRecepcionistaID(string RecepcionistaID)
        {
            return await _context.Cita.Where(e => e.RecepcionistaId.ToLower() == RecepcionistaID.ToLower()).ToListAsync();
        }

        public async Task<List<Cita>> ConsultarCitasPorServicioID(int ServicioID)
        {
            return await _context.Cita.Where(e => e.ServicioId == ServicioID).ToListAsync();
        }

        public async Task<List<Cita>> ConsultarCitasPorTallerID(int TallerID)
        {
            return await _context.Cita.Where(e => e.TallerId == TallerID).ToListAsync();
        }
    }
}
