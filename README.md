# Blog API

API REST para gestión de un blog con autenticación JWT y sistema de roles.

## 🛠️ Stack

- **Backend:** .NET 8, ASP.NET Core, Entity Framework Core
- **Base de datos:** SQL Server
- **Autenticación:** ASP.NET Identity + JWT en cookies HttpOnly
- **Documentación:** Swagger / OpenAPI

## ✨ Características

- CRUD de posts y categorías
- Autenticación con JWT almacenado en cookies HttpOnly y sistema de roles — Admin y User
- Solo el autor o un Admin puede editar/eliminar posts
- Solo Admin puede gestionar categorías
- Filtros por categoría y autor
- Paginación para los posts

## 🏗️ Arquitectura

- Controller → Service → Repository → Entity Framework → SQL Server
- Separación en capas con DTOs, Mappers e inyección de dependencias.

## Como instalar

### Requisitos

- .NET 8 SDK
- SQL Server

### Pasos para instalar el backend

1. Modificar appsettings.example.json a appsettings.json
2. Editar appsettings.json los valores de DefaultConnection para la coneción con la base de datos y Key para la codificación del token JWT.
3. Aplicar las migraciones mediante dotnet ef database update
4. Arrancar la API con dotnet run
5. Abrir Swagger para ver los endpoints http://localhost:5217/swagger
