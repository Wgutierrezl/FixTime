using TixTimeModels.Modelos;

namespace FixTimeBack.Interfaces
{
    public interface ICitasRepository
    {
        Task AddAppointment(Cita cita);
        Task UpdateAppoinment(Cita cita);
        Task<List<Cita>> GetAppointmentByUserId(string userId);
        Task<List<Cita>> GetAppointmentByGarageId(int garageId);
        Task<List<Cita>> GetAppointmenByServiceId(int serviceId);
        Task<Cita> GetAppointmentById(int id);
        Task<List<Cita>> GetAppointmentByRecepcionistId(string recepcionistid);
        Task SaveChanges();
    }
}
