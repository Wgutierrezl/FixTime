using FixTimeBack.Interfaces;
using TixTimeModels.Modelos;
using TixTimeModels.ModelosDTO;

namespace FixTimeBack.Service
{
    public class CitasService : ICitasService
    {
        private readonly ICitasRepository _repo;

        public CitasService(ICitasRepository repo)
        {
            _repo=repo;
        }
        public async Task<Cita> ActualizarCita(Cita cita, CitaDTO citaDTO)
        {
            cita.ServicioId= citaDTO.ServicioId;
            cita.VehiculoId= citaDTO.VehiculoId;
            cita.ClienteId= citaDTO.ClienteId;
            cita.FechaHora = citaDTO.FechaHora;
            cita.Estado = citaDTO.Estado;
            cita.TallerId=citaDTO.TallerId;
            cita.RecepcionistaId=citaDTO.RecepcionistaId;

            await _repo.UpdateAppoinment(cita);
            await _repo.SaveChanges();

            return cita;
        }

        public async Task<Cita> ActualizarEstadoCita(Cita cita, EstadoDTO estado)
        {
            cita.Estado = estado.Estado;

            await _repo.UpdateAppoinment(cita);
            await _repo.SaveChanges();

            return cita;
        }

        public async Task<Cita> AgregarCita(Cita cita)
        {
            await _repo.AddAppointment(cita);
            await _repo.SaveChanges();

            return cita;
        }

        public async Task<Cita> ConsultarCitaPorId(int citaID)
        {
            return await _repo.GetAppointmentById(citaID);
        }

        public async Task<List<Cita>> ConsultarCitasPorClienteID(string UsuarioID)
        {
            return await _repo.GetAppointmentByUserId(UsuarioID);
        }

        public async Task<List<Cita>> ConsultarCitasPorRecepcionistaID(string RecepcionistaID)
        {
            return await _repo.GetAppointmentByRecepcionistId(RecepcionistaID);
        }

        public async Task<List<Cita>> ConsultarCitasPorServicioID(int ServicioID)
        {
            return await _repo.GetAppointmenByServiceId(ServicioID);
        }

        public async Task<List<Cita>> ConsultarCitasPorTallerID(int TallerID)
        {
            return await _repo.GetAppointmentByGarageId(TallerID);
        }
    }
}
