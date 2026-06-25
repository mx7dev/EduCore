using EduCore.Business.Interfaces;
using EduCore.Business.Services;
using EduCore.Infrastructure.Data;
using EduCore.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ── Base de datos ──────────────────────────────────────
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// ── Repositorios ───────────────────────────────────────
builder.Services.AddScoped<IAlumnoRepository, AlumnoRepository>();

// ── Servicios de negocio ───────────────────────────────
builder.Services.AddScoped<AlumnoService>();

// ── API ────────────────────────────────────────────────
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();