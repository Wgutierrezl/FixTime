using FixTimeBack.Custom;
using FixTimeBack.Data;
using FixTimeBack.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TixTimeModels.Modelos;
using TixTimeModels.ModelosDTO;

namespace FixTimeBack.Repository
{
    public class RepositorioUsuarios : IUsuarioRepository
    {
        private readonly DataContext _context;

        public RepositorioUsuarios(DataContext context)
        {
            _context=context;
        }
        public async Task<Usuario> GetEmailUser(string email)
        {
            return await _context.Usuario.Where(e => e.CorreoElectronico == email).FirstOrDefaultAsync();
        }

        public async Task<Usuario> GetProfile(string userid)
        {
            return await _context.Usuario.FindAsync(userid);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task SignUser(Usuario usuario)
        {
            await _context.Usuario.AddAsync(usuario);
        }

        public Task UpdateUser(Usuario usuario)
        {
            _context.Entry(usuario).State = EntityState.Modified;
            return Task.CompletedTask;
        }
    }
}
