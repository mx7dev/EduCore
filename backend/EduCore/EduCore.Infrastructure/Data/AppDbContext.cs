using EduCore.Business.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduCore.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Alumno> Alumnos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alumno>(entity =>
            {
                entity.HasKey(a => a.Id);

                entity.Property(a => a.Codigo)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(a => a.Dni)
                    .IsRequired()
                    .HasMaxLength(8);

                entity.Property(a => a.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(a => a.ApellidoPaterno)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(a => a.ApellidoMaterno)
                    .HasMaxLength(50);

                entity.Property(a => a.Direccion)
                    .HasMaxLength(150);

                entity.Property(a => a.NumeroCelular)
                    .HasMaxLength(15);

                entity.Property(a => a.ContactoEmergenciaNombre)
                    .HasMaxLength(50);

                entity.Property(a => a.ContactoEmergenciaTelefono)
                    .HasMaxLength(15);

                entity.Property(a => a.ContactoEmergenciaRelacion)
                    .HasMaxLength(50);

                entity.Property(a => a.CorreoPersonal)
                    .HasMaxLength(100);

                entity.Property(a => a.CorreoInstitucional)
                    .HasMaxLength(100);

                entity.HasIndex(a => a.Dni)
                    .IsUnique();

                entity.HasIndex(a => a.Codigo)
                    .IsUnique();
            });
        }
    }
}
