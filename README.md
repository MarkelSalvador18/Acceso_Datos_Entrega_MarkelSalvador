# Acceso_Datos_Entrega_MarkelSalvador

API REST desarrollada con ASP.NET Core y Entity Framework Core para la gestion de actividades, tipos de actividad e instructores de un gimnasio. Proyecto correspondiente al modulo de Acceso a Datos (2º DAM).

## Tecnologias

- .NET 10 (C#)
- ASP.NET Core Web API
- Entity Framework Core
- Microsoft SQL Server
- OpenAPI / Swagger UI

## Requisitos previos

- .NET 10 SDK instalado.
- Instancia local de SQL Server en ejecucion.

## Configuracion y puesta en marcha

1. Clonar o descargar el repositorio.
2. Configurar la cadena de conexion en `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=4vGYM;Trusted_Connection=True;TrustServerCertificate=True;"
   }
   ```
3. Aplicar las migraciones para generar la base de datos:
   ```bash
   dotnet ef database update
   ```
4. (Opcional) Cargar datos de prueba ejecutando el script `InsertsGym.sql` en SQL Server.
5. Iniciar la aplicacion:
   ```bash
   dotnet run
   ```
6. Abrir la documentacion interactiva de Swagger en el navegador:
   `https://localhost:7198/swagger` o `http://localhost:5117/swagger` (segun el puerto asignado).

## Estructura del proyecto

- `Controllers/`: Controladores de la API para gestionar las peticiones HTTP.
- `Models/`: Entidades de la base de datos (Activity, ActivityType, Instructor).
- `Dtos/`: Objetos de transferencia de datos para la creacion y validacion de recursos.
- `Repositories/`: Capa de acceso a datos y logica de negocio.
- `Data/`: Configuracion del contexto de Entity Framework (`GymDbContext`).
- `Validations/`: Atributos personalizados para la validacion de fechas y reglas de negocio.
- `Migrations/`: Historial de migraciones de la base de datos.

## Endpoints principales

### Tipos de actividad (`/activity-types`)
- `GET /activity-types`: Obtiene todos los tipos de actividad registrados.
- `POST /activity-types`: Crea un nuevo tipo de actividad (valida que el nombre sea unico).

### Instructores (`/instructors`)
- `GET /instructors`: Obtiene el listado de instructores.
- `GET /instructors?id={id}`: Busca un instructor por su identificador.
- `GET /instructors?first_initial={letra}`: Filtra instructores por la inicial de su nombre.
- `POST /instructors`: Registra un nuevo instructor con validacion de datos (telefono numerico, email valido, formato de URL para la foto).

### Actividades (`/activities`)
- `POST /activities`: Crea una nueva actividad validando fechas, aforo, tipo de actividad asignado, monitores requeridos y control de solapamiento de horarios.
