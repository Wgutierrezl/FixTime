using TixTimeModels.Modelos;

namespace FixTimeBack.Interfaces
{
    public interface IVehiculoRepository
    {
        Task AddVehicule(Vehiculo vehiculo);
        Task UpdateVehicule(Vehiculo vehiculo);
        Task<Vehiculo> GetVehiculeById(int id);
        Task<List<Vehiculo>> GetVehiculeByUserId(string userId);

        Task SaveChanges();

    }
}
