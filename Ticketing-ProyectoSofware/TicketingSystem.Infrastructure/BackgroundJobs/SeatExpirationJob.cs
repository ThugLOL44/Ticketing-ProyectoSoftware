using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TicketingSystem.Domain.Enums;
using TicketingSystem.Domain.Entities;
using TicketingSystem.Infrastructure.Persistence;

namespace TicketingSystem.Infrastructure.BackgroundJobs;

public class SeatExpirationJob : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<SeatExpirationJob> _logger;

    private const int IntervalSeconds = 30;

    public SeatExpirationJob(
        IServiceScopeFactory scopeFactory,
        ILogger<SeatExpirationJob> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("SeatExpirationJob iniciado.");

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(TimeSpan.FromSeconds(IntervalSeconds), stoppingToken);
            await ProcessExpiredReservationsAsync(stoppingToken);
        }
    }

    private async Task ProcessExpiredReservationsAsync(CancellationToken ct)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var now = DateTimeOffset.UtcNow;
        var expiredReservations = await GetExpiredReservationsAsync(context, now, ct);

        if (!expiredReservations.Any())
        {
            _logger.LogDebug("SeatExpirationJob: sin reservas vencidas.");
            return;
        }

        _logger.LogInformation(
            "SeatExpirationJob: procesando {Count} reservas vencidas.",
            expiredReservations.Count);

        foreach (var reservation in expiredReservations)
            await ExpireReservationAsync(context, reservation, ct);
    }

    private static async Task<List<Reservation>> GetExpiredReservationsAsync(
        AppDbContext context,
        DateTimeOffset now,
        CancellationToken ct)
    {
        return await context.Reservations
            .Include(r => r.Seat)
            .Where(r => r.Status == "Pending" && r.ExpiresAt < now)
            .ToListAsync(ct);
    }

    private async Task ExpireReservationAsync(
        AppDbContext context,
        Reservation reservation,
        CancellationToken ct)
    {
        await using var tx = await context.Database.BeginTransactionAsync(ct);
        try
        {
            reservation.Status = "Expired";
            reservation.Seat.Status = SeatStatus.Available;

            context.AuditLogs.Add(BuildAuditLog(reservation));

            await context.SaveChangesAsync(ct);
            await tx.CommitAsync(ct);

            _logger.LogInformation(
                "Reserva {ReservationId} expirada. Butaca {SeatId} liberada.",
                reservation.Id,
                reservation.SeatId);
        }
        catch (Exception ex)
        {
            await tx.RollbackAsync(ct);
            _logger.LogError(ex,
                "Error al procesar reserva vencida {ReservationId}.",
                reservation.Id);
        }
    }

    private static AuditLog BuildAuditLog(Reservation reservation) => new()
    {
        Id = Guid.NewGuid(),
        Action = AuditAction.ReservationExpired,
        EntityType = "Reservation",
        EntityId = reservation.Id.ToString(),
        Details = $"Reserva {reservation.Id} expirada automáticamente. " +
                     $"Butaca {reservation.SeatId} liberada.",
        CreatedAt = DateTimeOffset.UtcNow
    };
}