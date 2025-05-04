using FixTimeBack.Interfaces;
using TixTimeModels.Modelos;
using TixTimeModels.ModelosDTO;

namespace FixTimeBack.Service
{
    public class TallerService : ITallerService
    {
        private readonly ITallerRepository _repo;

        public TallerService(ITallerRepository repo)
        {
            _repo=repo;
        }
        public async Task<Taller> ActualizarTaller(Taller taller, TallerDTO tallerDTO)
        {
            taller.AdministradorID = string.IsNullOrWhiteSpace(tallerDTO.AdministradorID) ? taller.AdministradorID : tallerDTO.AdministradorID;
            taller.Nombre = tallerDTO.Nombre;
            taller.Ubicacion = tallerDTO.Ubicacion;
            taller.HorarioAtencion = tallerDTO.HorarioAtencion;

            await _repo.UpdateGarage(taller);
            await _repo.SaveChanges();

            return taller;
        }

        public async Task<Taller> AgregarTaller(TallerDTO tallerDTO)
        {
            var taller = new Taller
            {
                Nombre = tallerDTO.Nombre,
                Ubicacion = tallerDTO.Ubicacion,
                HorarioAtencion = tallerDTO.HorarioAtencion,
                AdministradorID = tallerDTO.AdministradorID,

            };

            await _repo.AddGarage(taller);
            await _repo.SaveChanges();

            return taller;
        }

        public async Task<Taller> ObtenerTaller(int tallerid)
        {
            return await _repo.GetGarageById(tallerid);
        }

        public async Task<List<Taller>> ObtenerTalleres()
        {
            return await _repo.GetAllGarages();
        }

        public async Task<List<Taller>> ObtenerTalleresPorAdministrador(string Administradorid)
        {
            return await _repo.GetGarageByAdmin(Administradorid);
        }

        public async Task<List<Taller>> ObtenerTallerPorUbicacion(string Ubicacion)
        {
            return await _repo.GetGarageByLocation(Ubicacion);
        }
    }
}
