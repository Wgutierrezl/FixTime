﻿using FixTimeBack.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TixTimeModels.Modelos;
using TixTimeModels.ModelosDTO;

namespace FixTimeBack.Service
{
    public class CitasService : ICitasService
    {
        private readonly ICitasRepository _repo;
        private readonly IEmailService _emailService;
        private readonly IUsuarioRepository _userservice;
        private readonly IServicioRepository _servicioRepository;
        private readonly ITallerRepository _tallerRepository;
        private readonly IVehiculoRepository _vehiculoRepository;

        public CitasService(ICitasRepository repo, IEmailService emailService, IUsuarioRepository userservice,
            IServicioRepository servicioRepository, ITallerRepository tallerRepository, IVehiculoRepository vehiculoRepository)
        {
            _repo = repo;
            _emailService = emailService;
            _userservice = userservice;
            _servicioRepository = servicioRepository;
            _tallerRepository = tallerRepository;
            _vehiculoRepository = vehiculoRepository;
        }

        public async Task<Cita> ActualizarCita(Cita cita, CitaDTO citaDTO)
        {
            cita.ServicioId = citaDTO.ServicioId;
            cita.VehiculoId = citaDTO.VehiculoId;
            cita.ClienteId = citaDTO.ClienteId;
            cita.FechaHora = citaDTO.FechaHora;
            cita.Estado = citaDTO.Estado;
            cita.TallerId = citaDTO.TallerId;
            cita.RecepcionistaId = citaDTO.RecepcionistaId;

            await _repo.UpdateAppoinment(cita);
            await _repo.SaveChanges();

            return cita;
        }

        public async Task<Cita> ActualizarEstadoCita(Cita cita, EstadoDTO estado)
        {
            cita.Estado = estado.Estado;

            await _repo.UpdateAppoinment(cita);
            await _repo.SaveChanges();

            var usuario=await _userservice.GetProfile(cita.ClienteId);
            var email = usuario.CorreoElectronico;


            string emailTemplate =
               await System.IO.File.ReadAllTextAsync("EmailTemplates/EmailBookingChangeState.html");

            emailTemplate = emailTemplate
                .Replace("{NOMBRE_CLIENTE}", usuario.NombreCompleto)
                .Replace("{FECHA_HORA}", cita.FechaHora.ToString("f"))
                .Replace("{SERVICIO}", cita.Servicio.Nombre)
                .Replace("{TALLER}", cita.Taller.Nombre)
                .Replace("{VEHICULO}", $"{cita.Vehiculo.Marca} {cita.Vehiculo.Modelo}")
                .Replace("{ESTADO}", cita.Estado);

            string subject = $"{usuario.NombreCompleto} Se ha agendado correctamente tu cita";
            string body = "Tu cita quedo correctamente agendada en el taller";

            await _emailService.SendEmailAsync(email, subject, emailTemplate, true);


            return cita;
        }

        public async Task<Cita> AgregarCita(Cita cita)
        {
            await _repo.AddAppointment(cita);
            await _repo.SaveChanges();

            var email = await FindEmailByUserId(cita.ClienteId);
            var usuario = await _userservice.GetProfile(cita.ClienteId);
            var servicio = await _servicioRepository.GetServiceById(cita.ServicioId!.Value);
            var taller = await _tallerRepository.GetGarageById(cita.TallerId!.Value ); 
            var vehiculo = await _vehiculoRepository.GetVehiculeById(cita.VehiculoId!.Value);

            string emailTemplate =
                await System.IO.File.ReadAllTextAsync("EmailTemplates/EmailBookingConfirm.html");

            emailTemplate = emailTemplate
                .Replace("{NOMBRE_CLIENTE}", usuario.NombreCompleto)
                .Replace("{FECHA_HORA}", cita.FechaHora.ToString("f"))
                .Replace("{SERVICIO}", servicio.Nombre)
                .Replace("{TALLER}", taller.Nombre)
                .Replace("{VEHICULO}", $"{vehiculo.Marca} {vehiculo.Modelo}")
                .Replace("{ESTADO}", cita.Estado);

            string subject = $"{usuario.NombreCompleto} Se ha agendado correctamente tu cita";
            string body = "Tu cita quedo correctamente agendada en el taller";

            await _emailService.SendEmailAsync(email, subject, emailTemplate, true);

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
            var user = await _userservice.GetProfile(userid);
            return user.CorreoElectronico!;
        }
    }
}