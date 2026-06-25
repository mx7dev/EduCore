# EduCore 🎓

Sistema de gestión educativa construido con **.NET 10** y **Angular** aplicando arquitectura en capas.

## 🏗️ Arquitectura

Este proyecto aplica una arquitectura de 3 capas:

- **EduCore.API** — capa de presentación (Controllers, REST API)
- **EduCore.Business** — capa de negocio (Servicios, Entidades, Reglas, DTOs)
- **EduCore.Infrastructure** — capa de datos (Repositorios, Entity Framework Core)

```
┌─────────────────────────────────┐
│         EduCore.API             │
│   (Controllers, Swagger)        │
└────────────────┬────────────────┘
                 │
┌────────────────▼────────────────┐
│       EduCore.Business          │
│  (Services, Entities, DTOs)     │
└────────────────┬────────────────┘
                 │
┌────────────────▼────────────────┐
│     EduCore.Infrastructure      │
│  (Repositories, EF Core, BD)    │
└─────────────────────────────────┘
```

## 🛠️ Tecnologías

- .NET 10
- ASP.NET Core Web API
- Entity Framework Core (Code First)
- SQL Server
- Swagger / OpenAPI
- Angular (próximamente)

## ✅ Fases completadas

### Fase 1 — CRUD base
- Arquitectura en 3 capas
- Entidad `Alumno` con encapsulamiento y reglas de dominio
- Repository Pattern con interfaz desacoplada
- Inyección de dependencias (Scoped)
- DTOs para entrada (`CrearAlumnoDto`) y salida (`AlumnoDto`)
- Generación automática de código de alumno (año + correlativo)
- API REST con Swagger

### Fase 2 — Validaciones y manejo de errores
- FluentValidation para validar DTOs de entrada
- Excepciones personalizadas (`FunctionalException`, `TechnicalException`)
- Manejo de errores controlado en controllers
- Respuestas de error amigables con código, mensaje y transactionId
- Validación de DNI duplicado antes de guardar

## 🔜 Próximas fases

- **Fase 3** — Autenticación con JWT y roles
- **Fase 4** — Foto de perfil
- **Fase 5** — Historial de cambios y auditoría
- **Fase 6** — Notificaciones
- **Fase 7** — Reportes

## 🚀 Cómo ejecutar

### Requisitos previos
- .NET 10 SDK
- SQL Server (local o Docker)
- Visual Studio 2026 o VS Code

### Pasos

1. Clona el repositorio:
   ```bash
   git clone https://github.com/mx7dev/EduCore.git
   ```

2. Configura la cadena de conexión en `backend/EduCore/EduCore.API/appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost,1433;Database=EduCoreDb;User Id=sa;Password=TuPassword;TrustServerCertificate=True"
     }
   }
   ```

3. Ejecuta las migraciones:
   ```bash
   cd backend/EduCore/EduCore.API
   dotnet ef database update --project ../EduCore.Infrastructure --startup-project .
   ```

4. Corre la API:
   ```bash
   dotnet run --project backend/EduCore/EduCore.API
   ```

5. Abre Swagger en:
   ```
   https://localhost:7208/swagger
   ```

## 📐 Decisiones arquitectónicas

| Decisión | Alternativa considerada | Razón |
|---|---|---|
| Arquitectura en 3 capas | DDD, Hexagonal | Dominio simple, equipo pequeño, sin necesidad de over-engineering |
| Code First | Database First | Versionado de BD junto al código, fácil de clonar y ejecutar |
| Repository Pattern | Acceso directo con DbContext | Desacoplar lógica de negocio del acceso a datos |
| DTOs separados | Exponer entidades directamente | Seguridad, no exponer estructura interna de BD |
| Scoped lifetime | Singleton, Transient | Cada request HTTP tiene su propia instancia del DbContext |

## 📁 Estructura del proyecto

```
EduCore/
├── backend/
│   └── EduCore/
│       ├── EduCore.API/
│       │   ├── Controllers/
│       │   │   └── AlumnoController.cs
│       │   ├── appsettings.json
│       │   └── Program.cs
│       ├── EduCore.Business/
│       │   ├── DTOs/
│       │   │   ├── AlumnoDto.cs
│       │   │   └── CrearAlumnoDto.cs
│       │   ├── Entities/
│       │   │   └── Alumno.cs
│       │   ├── Interfaces/
│       │   │   └── IAlumnoRepository.cs
│       │   └── Services/
│       │       └── AlumnoService.cs
│       └── EduCore.Infrastructure/
│           ├── Data/
│           │   └── AppDbContext.cs
│           ├── Migrations/
│           └── Repositories/
│               └── AlumnoRepository.cs
└── frontend/                          ← próximamente
```
