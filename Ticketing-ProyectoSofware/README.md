# GrandLine Tickets

## Requisitos
- .NET 10 SDK
- SQL Server 2022+
- Instalar dotnet-ef globalmente: dotnet tool install --global dotnet-ef

## Clonar el repositorio
    git clone https://github.com/ThugLOL44/Ticketing-ProyectoSoftware.git
    cd Ticketing-ProyectoSoftware/Ticketing-ProyectoSofware

## Restaurar paquetes
Ejecutar antes de cualquier otro comando, especialmente si es la primera vez que clonás el proyecto:

    dotnet restore

## Configurar la base de datos
Abrir TicketingSystem.WebAPI/appsettings.json y cambiar el connection string según tu instancia de SQL Server:

    "DefaultConnection": "Server=localhost;Database=TicketingDB;Trusted_Connection=True;TrustServerCertificate=True;"

Si usás una instancia con nombre (ej. SQLEXPRESS):

    "DefaultConnection": "Server=TU_PC\\SQLEXPRESS;Database=TicketingDB;Trusted_Connection=True;TrustServerCertificate=True;"

## Ejecutar migraciones
    dotnet ef database update --project TicketingSystem.Infrastructure --startup-project TicketingSystem.WebAPI

## Compilar y ejecutar el backend
    dotnet run --project TicketingSystem.WebAPI --launch-profile http

El backend queda en http://localhost:5158 y el Swagger en http://localhost:5158/swagger

## Ejecutar el frontend
Opción A - Live Server desde VS Code:
    Click derecho en frontend/pages/index.html → Open with Live Server

Opción B - Python (si no tenés VS Code):
    cd frontend
    python -m http.server 5500
    Abrí http://localhost:5500/pages/index.html en el navegador