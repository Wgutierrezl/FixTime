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
            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Taller)
                .WithMany() // o .WithMany(t => t.Citas) si tu modelo Taller lo tiene
                .HasForeignKey(c => c.TallerId);

            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Vehiculo)
                .WithMany() // o .WithMany(v => v.Citas)
                .HasForeignKey(c => c.VehiculoId);

            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Servicio)
                .WithMany() // o .WithMany(s => s.Citas)
                .HasForeignKey(c => c.ServicioId);
        }
    }
}