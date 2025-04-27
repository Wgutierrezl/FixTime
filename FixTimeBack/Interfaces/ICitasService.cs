using TixTimeModels.Modelos;
using TixTimeModels.ModelosDTO;

namespace FixTimeBack.Interfaces
{
    public interface ICitasService
    {
        Task<Cita> AgregarCita(Cita cita);
        Task<Cita> ActualizarCita(Cita cita,CitaDTO citaDTO);
        Task<List<Cita>> ConsultarCitasPorClienteID(string UsuarioID);
        Task<List<Cita>> ConsultarCitasPorTallerID(int TallerID);
        Task<List<Cita>> ConsultarCitasPorServicioID(int ServicioID);
        Task<Cita> ConsultarCitaPorId(int citaID);
        Task<Cita> ActualizarEstadoCita(Cita cita,EstadoDTO estado);
        Task<List<Cita>> ConsultarCitasPorRecepcionistaID(string RecepcionistaID);
    }
}
