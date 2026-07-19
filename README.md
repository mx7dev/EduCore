# EduCore 🎓

Sistema de gestión educativa para colegios construido con **.NET 10** y **Angular 22** aplicando arquitectura en capas.

## 🏗️ Arquitectura Backend

Arquitectura en **3 capas (N-Tier) con influencias de Clean Architecture**:

- **EduCore.API** — capa de presentación (Controllers, REST API)
- **EduCore.Business** — capa de negocio (Servicios, Entidades, Reglas, DTOs)
- **EduCore.Infrastructure** — capa de datos (Repositorios, Entity Framework Core)

## 🏗️ Arquitectura Frontend

Estructura **Feature-based** con Angular 22:

```
src/app/
├── core/          ← guards, interceptors, services
├── shared/        ← componentes reutilizables
├── features/      ← módulos por funcionalidad
└── layout/        ← sidebar, navbar
```

## 🛠️ Tecnologías

**Backend:**
- .NET 10
- ASP.NET Core Web API
- Entity Framework Core (Code First)
- SQL Server
- Swagger / OpenAPI
- BCrypt.Net
- FluentValidation
- JWT + Refresh Token

**Frontend:**
- Angular 22 (Standalone Components)
- PrimeNG 22 + PrimeFlex (Community License)
- Signal Forms (`@angular/forms/signals`)
- `httpResource` para llamadas HTTP reactivas
- Signals para manejo de estado

## ✅ Fases completadas

### Fase 1 — CRUD base
- Arquitectura en 3 capas
- Entidad `Alumno` con encapsulamiento y reglas de dominio
- Repository Pattern con interfaz desacoplada
- Inyección de dependencias (Scoped)
- DTOs para entrada y salida
- Generación automática de código de alumno
- API REST con Swagger

### Fase 2 — Validaciones y manejo de errores
- FluentValidation para validar DTOs
- Excepciones personalizadas (`FunctionalException`, `TechnicalException`)
- Manejo de errores controlado en controllers
- Respuestas de error con código, mensaje y transactionId

### Fase 3 — Autenticación y Autorización
- JWT + Refresh Token
- Roles: Admin, Secretaria, Docente
- Endpoints protegidos con `[Authorize]`
- Seed Data con usuario Admin
- Hash de contraseñas con BCrypt
- ⚠️ Swagger UI tiene bug con JWT en .NET 10 + Swashbuckle 10.x — usar Postman

### Fase 4 — Módulos académicos (Backend)
- CRUD Profesores, Cursos, Periodos, Grados, Secciones
- Matrícula con validación de duplicado
- Notas por bimestre con libreta calculada automáticamente
- Proyecciones optimizadas en Secciones (sin over-fetching)

### Fase 5 — Frontend Angular 22
- Layout con Sidebar y Navbar
- Login conectado al backend con Toast de errores
- Auth Guard para protección de rutas
- Auth Interceptor para envío automático del token JWT
- Listado de alumnos con `httpResource` (Angular 22)
- Formulario de nuevo alumno con Signal Forms
- DatePicker en español con PrimeNG 22

## 🔜 Próximas fases

- **Fase 6** — Pantallas de Profesores, Cursos, Matrícula y Notas
- **Fase 7** — Foto de perfil
- **Fase 8** — Logout y gestión de sesión

## 🚀 Cómo ejecutar

### Backend

1. Configura `appsettings.json` con tu cadena de conexión y JwtSettings
2. Ejecuta migraciones:
   ```bash
   cd backend/EduCore/EduCore.API
   dotnet ef database update --project ../EduCore.Infrastructure --startup-project .
   ```
3. Corre la API:
   ```bash
   dotnet run --project backend/EduCore/EduCore.API
   ```

### Frontend

1. Copia `environment.example.ts` como `environment.ts` y configura tu license key de PrimeNG
2. Instala dependencias:
   ```bash
   cd frontend/educore-front
   npm install
   ```
3. Corre el frontend:
   ```bash
   ng serve
   ```
4. Abre `http://localhost:4200` — credenciales: `admin@educore.com` / `Admin123!`

## 📐 Decisiones arquitectónicas

| Decisión | Alternativa | Razón |
|---|---|---|
| 3 capas N-Tier | DDD, Hexagonal | Dominio simple, YAGNI |
| Sistema para colegio | Instituto | Dominio más acotado y predecible |
| Code First | Database First | Versionado junto al código |
| Repository Pattern | DbContext directo | Desacoplar datos del negocio |
| Scoped lifetime | Singleton | Cada request tiene su propio DbContext |
| JWT + Refresh Token | Solo JWT | Mejor UX sin sacrificar seguridad |
| httpResource | subscribe/RxJS | Estándar Angular 22, sin boilerplate |
| Signal Forms | ReactiveFormsModule | Estándar Angular 22 estable |
| PrimeNG 22 Community | Angular Material | Mejor diseño para sistemas enterprise |
| Swagger sobre Scalar | Scalar | Bug conocido en .NET 10 — migrar a Scalar en próximos proyectos |
| Proyecciones en Secciones | Include completo | Evitar over-fetching |
