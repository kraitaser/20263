# Proyecto 20263

Resumen
- API ASP.NET Core (.NET 10) para gestión de cuentas y autenticación mediante JWT.
- Contiene un controlador `AccountsController` con endpoints para registro, login y renovación de tokens.

Requisitos
- .NET 10 SDK
- SQL Server (o cadena de conexión compatible)
- Visual Studio 2026 / o `dotnet` CLI

Configuración importante
- `appsettings.json` debe incluir:
  - `ConnectionStrings:DefaultConnection` — cadena de conexión a la base de datos.
  - `LlaveJWT` — clave simétrica usada para firmar los JWT (mantener secreta).
- Nota: en `Program.cs` se lee `LlaveJWT` (con L mayúscula). En `AccountsController` se usa `configuration["llaveJWT"]` (con l minúscula). Unificar a una sola clave (recomendado: `LlaveJWT`) para evitar errores.

Endpoints principales (ruta base: `/api/accounts`)
- POST `/register` — registrar usuario. Recibe `UserCredentialsDTO` (email + password). Devuelve `AuthenticationResponseDTO` con `Token`, `expiration` y `UserId`.
- POST `/login` — iniciar sesión. Recibe `UserCredentialsDTO`. Devuelve `AuthenticationResponseDTO`.
- GET `/` — renovar token (endpoint protegido; requiere header `Authorization: Bearer <token>`). Devuelve nuevo `AuthenticationResponseDTO`.

Detalles técnicos
- Identity de ASP.NET Core para gestión de usuarios (`ApplicationUser`, `UserManager`, `SignInManager`).
- JWT configurado en `Program.cs` con `TokenValidationParameters`:
  - `ValidateIssuer` y `ValidateAudience` están a `false` (puede reforzarse para producción).
  - `ClockSkew = TimeSpan.Zero`.
- AutoMapper registrado.
- Swagger / OpenAPI disponible en entorno de desarrollo.

Cómo ejecutar
- Desde Visual Studio: abrir la solución `20263.slnx` y ejecutar (F5).
- Desde terminal:
  1. Restaurar paquetes: `dotnet restore`
  2. Aplicar migraciones (si aplica): `dotnet ef database update`
  3. Ejecutar: `dotnet run --project 20263`

Buenas prácticas / recomendaciones
- Guardar `LlaveJWT` en un secret store (Secret Manager, Azure Key Vault, variables de entorno).
- Establecer `Issuer` y `Audience` y activar su validación para producción.
- Revisar la coherencia del nombre de la clave JWT entre archivos (`LlaveJWT` vs `llaveJWT`).

Contacto
- Repositorio local: `C:\Users\ianga\source\repos\20263`
- Remote origin: `https://github.com/kraitaser/20263`