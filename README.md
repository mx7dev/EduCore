# EduCore 🎓

Sistema de gestión educativa para colegios construido con **.NET 10** y **Angular** aplicando arquitectura en capas.

## 🏗️ Arquitectura

Este proyecto aplica una arquitectura en **3 capas (N-Tier) con influencias de Clean Architecture**:

- **EduCore.API** — capa de presentación (Controllers, REST API)
- **EduCore.Business** — capa de negocio (Servicios, Entidades, Reglas, DTOs, Validadores)
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
- BCrypt.Net para hash de contraseñas
- FluentValidation
- JWT + Refresh Token
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

### Fase 3 — Autenticación y Autorización
- JWT (JSON Web Token) para autenticación
- Refresh Token para renovar sesión sin relogueo
- Roles: Admin, Secretaria, Docente
- Endpoints protegidos con `[Authorize]`
- Seed Data con usuario Admin inicial
- Hash de contraseñas con BCrypt
- ⚠️ **Nota**: Swagger UI tiene un bug conocido con JWT en .NET 10 + Swashbuckle 10.x donde el token no se envía en el header. Se recomienda usar Postman para probar endpoints protegidos, o migrar a Scalar en proyectos nuevos. Ver issues: [#3740](https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/3740) y [#3648](https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/3648)

### Fase 4 — Módulos académicos
- CRUD de Profesores con validaciones
- CRUD de Cursos con código autogenerado
- Periodos académicos con regla de solo un periodo activo
- Grados con enum NivelEducativo (Primaria/Secundaria) y validaciones de negocio
- Secciones con proyecciones optimizadas (sin Include, sin over-fetching)
- Matrícula de alumnos con validación de duplicado por periodo
- Registro de notas por bimestre (B1, B2, B3, B4)
- Libreta de notas con nota final calculada automáticamente

## 🔜 Próximas fases

- **Fase 5** — Foto de perfil del alumno
- **Fase 6** — Frontend Angular

## 🚀 Cómo ejecutar

### Requisitos previos
- .NET 10 SDK
- SQL Server (local o Docker)
- Visual Studio 2026 o VS Code
- Postman (para probar endpoints protegidos con JWT)

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
     },
     "JwtSettings": {
       "SecretKey": "TuSecretKeyMuyLargaYSegura123!",
       "Issuer": "EduCore.API",
       "Audience": "EduCore.Client",
       "ExpirationMinutes": 60
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

6. Para probar endpoints protegidos usa Postman:
   - `POST https://localhost:7208/api/Auth/login` con `{ "email": "admin@educore.com", "password": "Admin123!" }`
   - Copia el token y úsalo como Bearer Token en los siguientes requests

## 📐 Decisiones arquitectónicas

| Decisión | Alternativa considerada | Razón |
|---|---|---|
| Arquitectura en 3 capas | DDD, Hexagonal | Dominio simple, equipo pequeño, sin necesidad de over-engineering |
| Sistema para colegio | Instituto | Dominio más acotado y predecible, todos los colegios funcionan igual |
| Code First | Database First | Versionado de BD junto al código, fácil de clonar y ejecutar |
| Repository Pattern | Acceso directo con DbContext | Desacoplar lógica de negocio del acceso a datos |
| DTOs separados | Exponer entidades directamente | Seguridad, no exponer estructura interna de BD |
| Scoped lifetime | Singleton, Transient | Cada request HTTP tiene su propia instancia del DbContext |
| BCrypt en Business | Infrastructure | El hasheo es responsabilidad del negocio, no de la infraestructura |
| Swagger sobre Scalar | Scalar | Swagger es el estándar conocido; en proyectos futuros se usará Scalar por su compatibilidad nativa con .NET 10 |
| Refresh Token | Solo JWT | Mejor experiencia de usuario sin sacrificar seguridad |
| Proyecciones en Seccion | Include completo | Evitar over-fetching, traer solo los campos necesarios |
| Enum para Nivel y Turno | Tabla en BD | Valores fijos que no cambian — YAGNI |
| MatriculaId en Nota | AlumnoId + SeccionId | Garantiza que el alumno está matriculado antes de registrar nota |

## 📁 Estructura del proyecto

```
EduCore/
├── backend/
│   └── EduCore/
│       ├── EduCore.API/
│       │   ├── Controllers/
│       │   │   ├── AlumnoController.cs
│       │   │   ├── AuthController.cs
│       │   │   ├── CursoController.cs
│       │   │   ├── GradoController.cs
│       │   │   ├── MatriculaController.cs
│       │   │   ├── NotaController.cs
│       │   │   ├── PeriodoController.cs
│       │   │   ├── ProfesorController.cs
│       │   │   └── SeccionController.cs
│       │   ├── appsettings.json
│       │   └── Program.cs
│       ├── EduCore.Business/
│       │   ├── DTOs/
│       │   ├── Entities/
│       │   ├── Enums/
│       │   │   ├── Bimestre.cs
│       │   │   ├── NivelEducativo.cs
│       │   │   └── Turno.cs
│       │   ├── Exceptions/
│       │   ├── Interfaces/
│       │   ├── Services/
│       │   └── Validators/
│       └── EduCore.Infrastructure/
│           ├── Data/
│           │   └── AppDbContext.cs
│           ├── Migrations/
│           └── Repositories/
└── frontend/                          ← próximamente
```
