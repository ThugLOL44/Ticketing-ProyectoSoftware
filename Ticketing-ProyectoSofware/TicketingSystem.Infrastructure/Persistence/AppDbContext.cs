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
            var usuarioId = new Guid("44444444-4444-4444-4444-444444444444");

            var eventos = new[]
            {
        new { Id = new Guid("11111111-1111-1111-1111-111111111111"), Name = "Concierto de Rock",    Venue = "Estadio Luna Park",     Date = new DateTimeOffset(2026, 6,  15, 21,  0, 0, TimeSpan.Zero), ImageUrl = "https://images.unsplash.com/photo-1540039155733-5bb30b53aa14?w=400&q=80" },
        new { Id = new Guid("11111111-1111-1111-1111-222222222222"), Name = "Festival de Jazz",     Venue = "Teatro Colón",          Date = new DateTimeOffset(2026, 7,  22, 20,  0, 0, TimeSpan.Zero), ImageUrl = "https://images.unsplash.com/photo-1506157786151-b8491531f063?w=400&q=80" },
        new { Id = new Guid("11111111-1111-1111-1111-333333333333"), Name = "Electro Night",        Venue = "Club Niceto",           Date = new DateTimeOffset(2026, 8,   5, 23,  0, 0, TimeSpan.Zero), ImageUrl = "https://images.unsplash.com/photo-1574391884720-bbc3740c59d1?w=400&q=80" },
        new { Id = new Guid("11111111-1111-1111-1111-444444444444"), Name = "Stand Up Comedy",      Venue = "Teatro Gran Rex",       Date = new DateTimeOffset(2026, 8,  14, 21,  0, 0, TimeSpan.Zero), ImageUrl = "https://images.unsplash.com/photo-1585699324551-f6c309eedeca?w=400&q=80" },
        new { Id = new Guid("11111111-1111-1111-1111-555555555555"), Name = "Tributo a Queen",      Venue = "Movistar Arena",        Date = new DateTimeOffset(2026, 9,   1, 21, 30, 0, TimeSpan.Zero), ImageUrl = "https://images.unsplash.com/photo-1493225457124-a3eb161ffa5f?w=400&q=80" },
        new { Id = new Guid("11111111-1111-1111-1111-666666666666"), Name = "Tango Show",           Venue = "Café de los Angelitos", Date = new DateTimeOffset(2026, 9,  18, 20,  0, 0, TimeSpan.Zero), ImageUrl = "https://images.unsplash.com/photo-1545959570-a94084071b5d?w=400&q=80" },
        new { Id = new Guid("11111111-1111-1111-1111-777777777777"), Name = "Opera en Vivo",        Venue = "Teatro Argentino",      Date = new DateTimeOffset(2026, 10,  5, 20,  0, 0, TimeSpan.Zero), ImageUrl = "https://images.unsplash.com/photo-1507838153414-b4b713384a76?w=400&q=80" },
        new { Id = new Guid("11111111-1111-1111-1111-888888888888"), Name = "Festival de Folklore", Venue = "Estadio Obras",         Date = new DateTimeOffset(2026, 10, 22, 21,  0, 0, TimeSpan.Zero), ImageUrl = "https://images.unsplash.com/photo-1464375117522-1311d6a5b81f?w=400&q=80" },
        new { Id = new Guid("11111111-1111-1111-1111-999999999999"), Name = "Noche de Cumbia",      Venue = "Estadio Vélez",         Date = new DateTimeOffset(2026, 11,  8, 22,  0, 0, TimeSpan.Zero), ImageUrl = "https://images.unsplash.com/photo-1516450360452-9312f5e86fc7?w=400&q=80" },
    };

            modelBuilder.Entity<Event>().HasData(eventos.Select(e => new Event
            {
                Id = e.Id,
                Name = e.Name,
                Venue = e.Venue,
                EventDate = e.Date,
                Status = "Active",
                ImageUrl = e.ImageUrl
            }).ToArray());

            var sectores = new List<Sector>();
            var seats = new List<Seat>();

            for (int ei = 0; ei < eventos.Length; ei++)
            {
                var eventoId = eventos[ei].Id;
                var prefix = (ei + 1).ToString("D2");

                var generalId = new Guid($"{prefix}222222-2222-2222-2222-222222222222");
                var vipId = new Guid($"{prefix}333333-3333-3333-3333-333333333333");

                sectores.Add(new Sector { Id = generalId, EventId = eventoId, Name = "General", Price = 5000m, Capacity = 50 });
                sectores.Add(new Sector { Id = vipId, EventId = eventoId, Name = "VIP", Price = 15000m, Capacity = 50 });

                for (int numero = 1; numero <= 50; numero++)
                {
                    string fila = ((char)('A' + (numero - 1) / 10)).ToString();

                    seats.Add(new Seat
                    {
                        Id = new Guid($"{prefix}a00000-0000-0000-0000-{numero:D12}"),
                        SectorId = generalId,
                        RowIdentifier = fila,
                        SeatNumber = numero,
                        Status = SeatStatus.Available,
                        Version = 0
                    });

                    seats.Add(new Seat
                    {
                        Id = new Guid($"{prefix}b00000-0000-0000-0000-{numero:D12}"),
                        SectorId = vipId,
                        RowIdentifier = fila,
                        SeatNumber = numero,
                        Status = SeatStatus.Available,
                        Version = 0
                    });
                }
            }

            modelBuilder.Entity<Sector>().HasData(sectores.ToArray());
            modelBuilder.Entity<Seat>().HasData(seats.ToArray());

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = usuarioId,
                Name = "Usuario Test",
                Email = "test@ticketing.com",
                PasswordHash = "HASH_SIMULADO"
            });
        }

    }
}