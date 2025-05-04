using TixTimeModels.Modelos;

namespace FixTimeBack.Interfaces
{
    public interface ITallerRepository
    {
        Task AddGarage(Taller taller);
        Task UpdateGarage(Taller taller);   
        Task<List<Taller>> GetAllGarages();
        Task<Taller> GetGarageById(int id);
        Task<List<Taller>> GetGarageByAdmin(string userid);
        Task SaveChanges();
        Task<List<Taller>> GetGarageByLocation(string location);    
    }
}
