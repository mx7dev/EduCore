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
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Periodo> Periodos { get; set; }
        public DbSet<Grado> Grados { get; set; }

        public DbSet<Seccion> Secciones { get; set; }


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

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.Property(u => u.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(u => u.Apellido)
                    .HasMaxLength(100);

                entity.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(u => u.PasswordHash)
                    .IsRequired();

                entity.Property(u => u.Rol)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasIndex(u => u.Email)
                    .IsUnique();
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(r => r.Id);

                entity.Property(r => r.Token)
                    .IsRequired();

                entity.HasOne(r => r.Usuario)
                    .WithMany()
                    .HasForeignKey(r => r.UsuarioId);
            });

            modelBuilder.Entity<Usuario>().HasData(new
            {
                Id = 1,
                Nombre = "Admin",
                Apellido = "EduCore",
                Email = "admin@educore.com",
                PasswordHash = "$2a$10$h1tpPwycbaWgzOCPwWkVZ./BZ/1y03bpTGwNJeDuqDEr2FD9tQJg6",
                Rol = "Admin",
                Activo = true
            });

            modelBuilder.Entity<Profesor>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Dni)
                    .IsRequired()
                    .HasMaxLength(8);

                entity.Property(p => p.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(p => p.ApellidoPaterno)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(p => p.ApellidoMaterno)
                    .HasMaxLength(50);

                entity.Property(p => p.Especialidad)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(p => p.CorreoElectronico)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(p => p.Direccion)
                    .HasMaxLength(150);

                entity.Property(p => p.NumeroCelular)
                    .HasMaxLength(15);

                entity.HasIndex(p => p.Dni)
                    .IsUnique();

                entity.HasIndex(p => p.CorreoElectronico)
                    .IsUnique();
            });

            modelBuilder.Entity<Curso>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Codigo)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(c => c.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(c => c.Descripcion)
                    .HasMaxLength(500);

                entity.HasIndex(c => c.Codigo)
                    .IsUnique();

                entity.HasIndex(c => c.Nombre)
                    .IsUnique();
            });

            modelBuilder.Entity<Periodo>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Año)
                    .IsRequired();

                entity.Property(p => p.Descripcion)
                    .HasMaxLength(200);

                entity.HasIndex(p => p.Año)
                    .IsUnique();
            });

            modelBuilder.Entity<Grado>(entity =>
            {
                entity.HasKey(g => g.Id);

                entity.Property(g => g.Numero)
                    .IsRequired();

                entity.Property(g => g.Nivel)
                    .IsRequired();

                entity.HasIndex(g => new { g.Numero, g.Nivel })
                    .IsUnique();
            });

            modelBuilder.Entity<Seccion>(entity =>
            {
                entity.HasKey(s => s.Id);

                entity.Property(s => s.Nombre)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(s => s.Turno)
                    .IsRequired();

                entity.HasOne(s => s.Grado)
                    .WithMany()
                    .HasForeignKey(s => s.GradoId);

                entity.HasOne(s => s.Periodo)
                    .WithMany()
                    .HasForeignKey(s => s.PeriodoId);

                entity.HasIndex(s => new { s.Nombre, s.GradoId, s.PeriodoId })
                    .IsUnique();
            });
        }
    }
}
