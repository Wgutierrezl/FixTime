using Microsoft.EntityFrameworkCore;
using TixTimeModels.Modelos;

namespace FixTimeBack.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Cita> Cita { get; set; }
        public DbSet<Vehiculo> Vehiculo { get; set; }
        public DbSet<Taller> Taller { get; set; }
        public DbSet<Servicio> Servicio { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
