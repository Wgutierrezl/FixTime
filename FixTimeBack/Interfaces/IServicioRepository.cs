using TixTimeModels.Modelos;
using TixTimeModels.ModelosDTO;

namespace FixTimeBack.Interfaces
{
    public interface IServicioRepository
    {
        Task AddService(Servicio servicio);
        Task UpdateService(Servicio servicio);
        Task<Servicio> GetServiceById(int id);
        Task<List<Servicio>> GetServices();

        Task<List<Servicio>> GetServiceByGarageId(int garageId);
        Task SaveChanges();
    }
}
