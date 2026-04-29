# GrandLine Tickets
Sistema de reserva de tickets - Clean Architecture .NET - Proyecto Software 2026

## Requisitos
- .NET 10 SDK
- SQL Server 2022+
- Instalar dotnet-ef globalmente: dotnet tool install --global dotnet-ef

## Configurar la base de datos
Abrir TicketingSystem.WebAPI/appsettings.json y cambiar el connection string según tu instancia de SQL Server:

    "DefaultConnection": "Server=localhost;Database=TicketingDB;Trusted_Connection=True;TrustServerCertificate=True;"

## Ejecutar migraciones
    dotnet ef database update --project TicketingSystem.Infrastructure --startup-project TicketingSystem.WebAPI

## Compilar y ejecutar el backend
    dotnet run --project TicketingSystem.WebAPI --launch-profile http

El backend queda en http://localhost:5158 y el Swagger en http://localhost:5158/swagger

## Ejecutar el frontend
Abrir frontend/pages/index.html con Live Server desde VS Code.