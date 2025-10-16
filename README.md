## MexNature API

## Características

* **Gestión de Lugares**: Operaciones CRUD para lugares naturales, incluyendo coordenadas y metadatos.
* **Relaciones de Datos**: Modelo de datos relacional que incluye senderos, fotos, reseñas y servicios por lugar.
* **Filtrado de API**: El endpoint principal permite filtrar lugares por categoría.
* **Base de Datos en Contenedor**: La base de datos SQL Server se ejecuta en un contenedor de Docker para un entorno de desarrollo aislado y reproducible.
* **Entity Framework Core**: Uso de EF Core para el mapeo objeto-relacional (ORM), migraciones y carga de datos iniciales (seed).
* **Documentación Interactiva**: Interfaz de Swagger UI para visualizar y probar los endpoints de la API.

---

## Se usó:

* **Framework**: .NET 8
* **Base de Datos**: SQL Server (vía Docker)
* **ORM**: Entity Framework Core 8
* **Arquitectura**: API RESTful
* **Contenedores**: Docker

---

### Prerrequisitos

* [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
* [Docker Desktop](https://www.docker.com/products/docker-desktop/)

### Guía de Instalación

1.  **Clona el repositorio:**
    ```bash
    git clone [https://github.com/tu-usuario/MexNature-Api.git](https://github.com/tu-usuario/MexNature-Api.git)
    cd MexNature-Api
    ```

2.  **Levanta la base de datos con Docker:**
    Abre una terminal y ejecuta el siguiente comando para crear e iniciar el contenedor de SQL Server.
    ```bash
    docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=tuPasswordSeguro123" \
    -p 1433:1433 --name sqlserver-mexnat -d [mcr.microsoft.com/mssql/server:2022-latest](https://mcr.microsoft.com/mssql/server:2022-latest)
    ```

3.  **Configura la cadena de conexión:**
    Abre el archivo `MexNature.Api/appsettings.json` y asegúrate de que la `DefaultConnection` apunte a tu contenedor Docker. **Importante**: La contraseña (`Password`) debe ser la misma que usaste en el comando de Docker.
    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Server=localhost,1433;Database=MexNatureDB;User Id=sa;Password=tuPasswordSeguro123;TrustServerCertificate=True"
      }
    }
    ```

4.  **Aplica las migraciones de EF Core:**
    Navega a la carpeta del proyecto y ejecuta el comando para que Entity Framework construya la base de datos y la pueble con los datos iniciales.
    ```bash
    cd MexNature.Api
    dotnet ef database update
    ```
    *(Si no tienes `dotnet-ef`, instálalo con `dotnet tool install --global dotnet-ef`)*.

5.  **Ejecuta la aplicación:**
    ¡Ya está todo listo! Ejecuta la API con el siguiente comando:
    ```bash
    dotnet run
    ```
    La API estará disponible en la URL que indique la terminal (ej. `http://localhost:5141`).

---

## Endpoints de la API

Una vez que la aplicación esté corriendo, puedes acceder a la interfaz de Swagger en `http://localhost:[PUERTO]/swagger` para probar los siguientes endpoints:

#### `GET /api/places`
Obtiene una lista de todos los lugares naturales.
* **Parámetros (Query):**
    * `category` (string, opcional): Filtra los lugares por una categoría específica (ej. "Cascada", "Parque").
* **Respuesta Exitosa (200 OK):**
    ```json
    [
      {
        "id": 1,
        "name": "Cascadas de Agua Azul",
        "category": "Cascada",
        "latitude": 17.2586,
        "longitude": -92.1158
      }
    ]
    ```

#### `GET /api/places/{id}`
Obtiene el detalle completo de un lugar específico por su ID.
* **Parámetros (Ruta):**
    * `id` (integer, requerido): El ID del lugar a obtener.
* **Respuesta Exitosa (200 OK):** Devuelve el objeto completo del lugar con sus relaciones (senderos, fotos, etc.).

#### `POST /api/places`
Crea un nuevo lugar natural.
* **Cuerpo de la Petición (Request Body):**
    ```json
    {
      "name": "Bosque de la Primavera",
      "description": "Área natural protegida...",
      "category": "bosque",
      "latitude": 20.6667,
      "longitude": -103.5,
      "entryFee": 50,
      "openingHours": "08:00-18:00"
    }
    ```
* **Respuesta Exitosa (201 Created):** Devuelve el objeto del lugar recién creado, incluyendo el `Id` asignado por la base de datos.
