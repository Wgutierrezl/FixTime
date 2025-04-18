using FixTimeBack.Data;
using Microsoft.EntityFrameworkCore;
using TixTimeModels.Modelos;
using TixTimeModels.ModelosDTO;

namespace FixTimeBack.Interfaces
{
    public class RepositorioTaller : IServiciosTaller
    {
        private readonly DataContext _context;
        public RepositorioTaller(DataContext context)
        {
            _context=context;
        }
        public async Task<Taller> ActualizarTaller(Taller taller, TallerDTO tallerDTO)
        {
            taller.Nombre = tallerDTO.Nombre;
            taller.Ubicacion=tallerDTO.Ubicacion;
            taller.HorarioAtencion = tallerDTO.HorarioAtencion;
            taller.AdministradorID=tallerDTO.AdministradorID;

            _context.Entry(taller).State=EntityState.Modified;
            await _context.SaveChangesAsync();
            return taller;
        }

        public async Task<Taller> AgregarTaller(TallerDTO tallerDTO)
        {
            var taller = new Taller
            {
                Nombre = tallerDTO.Nombre,
                Ubicacion = tallerDTO.Ubicacion,
                HorarioAtencion = tallerDTO.HorarioAtencion,
                AdministradorID = tallerDTO.AdministradorID

            };

            _context.Taller.Add(taller);
            await _context.SaveChangesAsync();
            return taller;
        }

        public async Task<Taller> ObtenerTaller(int tallerid)
        {
            return await _context.Taller.FindAsync(tallerid);
        }

        public async Task<List<Taller>> ObtenerTalleres()
        {
            return await _context.Taller.ToListAsync();
        }

        public async Task<List<Taller>> ObtenerTalleresPorAdministrador(string Administradorid)
        {
            return await _context.Taller.Where(e => e.AdministradorID.ToLower() == Administradorid.ToLower()).ToListAsync();
            
        }

        public async Task<List<Taller>> ObtenerTallerPorUbicacion(string Ubicacion)
        {
            return await _context.Taller.Where(e => e.Ubicacion.ToLower() == Ubicacion.ToLower()).ToListAsync();
        }
    }
}
