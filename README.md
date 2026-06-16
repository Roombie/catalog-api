# Catálogo / Inventario — Aplicación web full stack

Aplicación full stack para gestión de catálogo e inventario, construida como
proyecto personal para demostrar un ciclo completo: API REST en .NET, base de
datos relacional, autenticación con JWT y un cliente SPA en Angular.

> **Reemplaza este bloque cuando termines:** una o dos frases tuyas explicando
> qué resuelve el proyecto y por qué lo construiste. Agrega 2–3 capturas de
> pantalla (login, listado de productos, reporte de bajo stock).

## Stack

| Capa        | Tecnología                                              |
|-------------|---------------------------------------------------------|
| Backend     | ASP.NET Core Web API (C#, .NET 8), arquitectura en capas |
| Datos       | SQL Server + Entity Framework Core + stored procedure   |
| Auth        | JWT (JSON Web Tokens) + hashing con BCrypt               |
| Frontend    | Angular (SPA)                                            |
| CI          | GitHub Actions (build + test en cada push)              |

## Arquitectura

```
Angular SPA  ──HTTP/JWT──►  ASP.NET Core Web API
                                │
                  Controller → Service → Repository
                                │
                          EF Core  ──►  SQL Server
                                          │
                                  sp_LowStockReport (stored procedure)
```

Separación en capas (controller / service / repository) para mantener bajo
acoplamiento y cumplir con principios SOLID. Las contraseñas se almacenan
hasheadas con BCrypt; nunca en texto plano. Los reportes que requieren lógica
de consulta se resuelven con un stored procedure invocado de forma
parametrizada (sin riesgo de inyección SQL).

## Funcionalidades

- Registro e inicio de sesión con emisión de JWT.
- CRUD de categorías y productos (productos relacionados a una categoría).
- Endpoints de escritura protegidos por autenticación.
- Reporte de productos con bajo stock mediante stored procedure.

## Cómo correrlo localmente

Requisitos: .NET 8 SDK, Node 20+, Angular CLI, SQL Server.

```bash
# 1) Base de datos
#    Ejecuta database/schema.sql en tu instancia de SQL Server.

# 2) Backend
cd backend/src/CatalogApi
# ajusta la cadena de conexión y Jwt:Key en appsettings.json
dotnet run

# 3) Frontend
cd frontend
npm install
ng serve
```

API: `https://localhost:7001/swagger` · App: `http://localhost:4200`

## Estructura del repositorio

```
backend/    API en .NET (Controllers, Services, Repositories, Data, Models)
frontend/   SPA en Angular
database/   schema.sql (tablas, seed, stored procedure)
.github/    workflow de CI
```

## Autor

Enger Eduardo González Méndez — Ingeniero de Sistemas
GitHub: https://github.com/Roombie
