using TixTimeModels.Modelos;

namespace FixTimeBack.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario> GetEmailUser(string email);
        Task SignUser(Usuario usuario);
        Task<Usuario> GetProfile(string userid);
        Task UpdateUser(Usuario usuario);
        Task SaveChanges();
        
    }
}
