using TixTimeModels.Modelos;
using TixTimeModels.ModelosDTO;

namespace FixTimeBack.Interfaces
{
    public interface IServicioService
    {
        Task<Servicio> AgregarServicios(Servicio servicios);
        Task<Servicio> ActualizarServicio(Servicio servicios,ServiciosDTO serviciosDTO);
        Task<List<Servicio>> ObtenerServiciosPorTallerId(int TallerId);
        Task<Servicio> ObtenerServicioPorId(int id);
    }
}
