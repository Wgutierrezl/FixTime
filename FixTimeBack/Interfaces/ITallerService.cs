using TixTimeModels.Modelos;
using TixTimeModels.ModelosDTO;

namespace FixTimeBack.Interfaces
{
    public interface ITallerService
    {
        Task<Taller> AgregarTaller(TallerDTO tallerDTO);
        Task<Taller> ActualizarTaller(Taller taller, TallerDTO tallerDTO);
        Task<List<Taller>> ObtenerTalleres();
        Task<Taller> ObtenerTaller(int tallerid);
        Task<List<Taller>> ObtenerTalleresPorAdministrador(string Administradorid);
        Task<List<Taller>> ObtenerTallerPorUbicacion(string Ubicacion);
    }
}
