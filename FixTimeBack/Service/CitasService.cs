using FixTimeBack.Interfaces;
using TixTimeModels.Modelos;
using TixTimeModels.ModelosDTO;

namespace FixTimeBack.Service
{
    public class CitasService : ICitasService
    {
        private readonly ICitasRepository _repo;
        private readonly IEmailService _emailService;
        private readonly IUsuarioRepository _userservice;

        public CitasService(ICitasRepository repo, IEmailService emailService, IUsuarioRepository userservice)
        {
            _repo = repo;
            _emailService = emailService;
            _userservice = userservice; 
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

            var email = await FindEmailByUserId(cita.ClienteId);

            string subject = $"{cita.ClienteId} Se ha agendado correctamente tu cita";
            string body = "Tu cita quedo correctamente agendada en el taller";

            await _emailService.SendEmailAsync(email, subject, body);
            
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

        private async Task<string> FindEmailByUserId(string userid)
        {
            var user=await _userservice.GetProfile(userid);
            return user.CorreoElectronico!;
        }
    }
}
