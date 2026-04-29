using TicketingSystem.Domain.Entities;
using TicketingSystem.Domain.Enums;

namespace TicketingSystem.Infrastructure.Persistence
{
    public static class SeedData
    {
            public static void Initialize(AppDbContext context)
            {
            if (context.Events.Any()) return;

            var eventoId = Guid.NewGuid();
            var sectorGeneralId = Guid.NewGuid();
            var sectorVipId = Guid.NewGuid();

            var evento = new Event
            {
                Id = eventoId,
                Name = "Concierto de Rock",
                Venue = "Estadio Luna Park",
                EventDate = DateTimeOffset.UtcNow.AddDays(30),
                Status = "Active"
            };

            var sectorGeneral = new Sector
            {
                Id = sectorGeneralId,
                EventId = eventoId,
                Name = "General",
                Price = 5000m,
                Capacity = 50
            };

            var sectorVip = new Sector
            {
                Id = sectorVipId,
                EventId = eventoId,
                Name = "VIP",
                Price = 15000m,
                Capacity = 50
            };

            context.Events.Add(evento);
            context.Sectors.AddRange(sectorGeneral, sectorVip);

            var butacas = new List<Seat>();

            for (int numero = 1; numero <= 50; numero++)
            {
                butacas.Add(new Seat
                {
                    Id = Guid.NewGuid(),
                    SectorId = sectorGeneralId,
                    RowIdentifier = ObtenerFila(numero),
                    SeatNumber = numero,
                    Status = SeatStatus.Available,
                    Version = 0
                });

                butacas.Add(new Seat
                {
                    Id = Guid.NewGuid(),
                    SectorId = sectorVipId,
                    RowIdentifier = ObtenerFila(numero),
                    SeatNumber = numero,
                    Status = SeatStatus.Available,
                    Version = 0
                });
            }

            context.Seats.AddRange(butacas);
            context.SaveChanges();
        }

        private static string ObtenerFila(int numeroButaca)
        {
            int indiceFila = (numeroButaca - 1) / 10;
            return ((char)('A' + indiceFila)).ToString();
        }
    }
}