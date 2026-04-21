using Microsoft.EntityFrameworkCore;
using TicketingSystem.Domain.Entities;
using TicketingSystem.Domain.Enums;

namespace TicketingSystem.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Event> Events { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            SeedDatabase(modelBuilder);
        }

        private static void SeedDatabase(ModelBuilder modelBuilder)
        {
            var eventoId = new Guid("11111111-1111-1111-1111-111111111111");
            var sectorGeneralId = new Guid("22222222-2222-2222-2222-222222222222");
            var sectorVipId = new Guid("33333333-3333-3333-3333-333333333333");
            var usuarioId = new Guid("44444444-4444-4444-4444-444444444444");

            modelBuilder.Entity<Event>().HasData(new Event
            {
                Id = eventoId,
                Name = "Concierto de Rock",
                Venue = "Estadio Luna Park",
                EventDate = new DateTimeOffset(2026, 6, 15, 21, 0, 0, TimeSpan.Zero),
                Status = "Active"
            });

            modelBuilder.Entity<Sector>().HasData(
                new Sector
                {
                    Id = sectorGeneralId,
                    EventId = eventoId,
                    Name = "General",
                    Price = 5000m,
                    Capacity = 50
                },
                new Sector
                {
                    Id = sectorVipId,
                    EventId = eventoId,
                    Name = "VIP",
                    Price = 15000m,
                    Capacity = 50
                }
            );

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = usuarioId,
                Name = "Usuario Test",
                Email = "test@ticketing.com",
                PasswordHash = "hash_simulado"
            });

            var seats = new List<Seat>();
            for (int numero = 1; numero <= 50; numero++)
            {
                string fila = ((char)('A' + (numero - 1) / 10)).ToString();

                // GUIDs deterministas — siempre el mismo valor para cada butaca
                seats.Add(new Seat
                {
                    Id = new Guid($"aa000000-0000-0000-0000-{numero:D12}"),
                    SectorId = sectorGeneralId,
                    RowIdentifier = fila,
                    SeatNumber = numero,
                    Status = SeatStatus.Available,
                    Version = 0
                });

                seats.Add(new Seat
                {
                    Id = new Guid($"bb000000-0000-0000-0000-{numero:D12}"),
                    SectorId = sectorVipId,
                    RowIdentifier = fila,
                    SeatNumber = numero,
                    Status = SeatStatus.Available,
                    Version = 0
                });
            }

            modelBuilder.Entity<Seat>().HasData(seats);
        }
    }
}