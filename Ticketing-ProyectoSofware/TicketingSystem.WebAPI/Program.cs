using Microsoft.EntityFrameworkCore;
using TicketingSystem.Application.Interfaces;
using TicketingSystem.Application.UseCases;
using TicketingSystem.Application.Services;
using TicketingSystem.Infrastructure.Persistence;
using TicketingSystem.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


builder.Services.AddDbContext<AppDbContext>(options =>

    options.UseSqlServer(

        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("TicketingSystem.Infrastructure")

    )

);


builder.Services.AddScoped<ISeatsRepository, SeatsRepository>();
builder.Services.AddScoped<IReservationsRepository, ReservationsRepository>();
builder.Services.AddScoped<IAuditLogRepository, AuditLogRepository>();

builder.Services.AddScoped<GetSeatsByEventUseCase>();
builder.Services.AddScoped<CreateReservationUseCase>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IEventService, EventService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())

{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();