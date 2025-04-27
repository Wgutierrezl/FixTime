using FixTimeBack.Data;
using FixTimeBack.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TixTimeModels.Modelos;
using TixTimeModels.ModelosDTO;

namespace FixTimeBack.Repository
{
    public class RepositorioServicios : IServicioService
    {
        private readonly DataContext _context;

        public RepositorioServicios(DataContext context)
        {
            _context = context;
        }
        public async Task<Servicio> ActualizarServicio(Servicio servicios, ServiciosDTO serviciosDTO)
        {
            servicios.Nombre = serviciosDTO.Nombre;
            servicios.Precio = serviciosDTO.Precio;
            servicios.Descripcion = serviciosDTO.Descripcion;
            servicios.TallerId = serviciosDTO.TallerId ?? null;

            _context.Entry(servicios).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return servicios;
        }

        public async Task<Servicio> AgregarServicios(Servicio servicios)
        {
            _context.Servicio.Add(servicios);
            await _context.SaveChangesAsync();
            return servicios;
        }

        public async Task<Servicio> ObtenerServicioPorId(int id)
        {
            return await _context.Servicio.FindAsync(id);
        }

        public async Task<List<Servicio>> ObtenerServiciosPorTallerId(int TallerId)
        {
            return await _context.Servicio.Where(e => e.TallerId == TallerId).ToListAsync();
        }
    }
}
