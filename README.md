# EduCore 🎓

Sistema de gestión educativa construido con **.NET 10** y **Angular** aplicando arquitectura en capas.

## 🏗️ Arquitectura

Este proyecto aplica una arquitectura de 3 capas:

- **EduCore.API** — capa de presentación (Controllers, REST API)
- **EduCore.Business** — capa de negocio (Servicios, Entidades, Reglas)
- **EduCore.Infrastructure** — capa de datos (Repositorios, Entity Framework)

## 🛠️ Tecnologías

- .NET 10
- ASP.NET Core Web API
- Entity Framework Core (Code First)
- SQL Server
- Angular (próximamente)

## 🚀 Cómo ejecutar

1. Clona el repositorio
2. Configura tu cadena de conexión en `appsettings.json`
3. Ejecuta las migraciones:
```bash
   dotnet ef database update --project backend/EduCore/EduCore.Infrastructure --startup-project backend/EduCore/EduCore.API
```
4. Corre la API:
```bash
   dotnet run --project backend/EduCore/EduCore.API
```

## 📚 Decisiones arquitectónicas

- Se eligió arquitectura en capas sobre DDD/Hexagonal porque el dominio es simple y el equipo es pequeño
- Code First para mantener el versionado de BD junto al código
- Repository Pattern para desacoplar el acceso a datos de la lógica de negocio
