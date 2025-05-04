using FixTimeBack.Interfaces;
using TixTimeModels.Modelos;
using TixTimeModels.ModelosDTO;

namespace FixTimeBack.Service
{
    public class ServiciosService : IServicioService
    {
        private readonly IServicioRepository _repo;

        public ServiciosService(IServicioRepository repo)
        {
            _repo = repo;
        }
        public async Task<Servicio> ActualizarServicio(Servicio servicios, ServiciosDTO serviciosDTO)
        {
            servicios.Nombre = serviciosDTO.Nombre;
            servicios.Precio = serviciosDTO.Precio;
            servicios.Descripcion = serviciosDTO.Descripcion;
            servicios.TallerId=serviciosDTO.TallerId;

            await _repo.UpdateService(servicios);
            await _repo.SaveChanges();

            return servicios;
            
        }

        public async Task<Servicio> AgregarServicios(Servicio servicios)
        {
            await _repo.AddService(servicios);
            await _repo.SaveChanges();

            return servicios;
        }

        public async Task<Servicio> ObtenerServicioPorId(int id)
        {
            return await _repo.GetServiceById(id);

        }

        public async Task<List<Servicio>> ObtenerServiciosPorTallerId(int TallerId)
        {
            return await _repo.GetServiceByGarageId(TallerId);
        }
    }
}
