using TicketingSystem.Domain.Entities;
using TicketingSystem.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TicketingSystem.Infrastructure.Persistence.Mappings
{   /// </summary>
    /// Version actúa como token de concurrencia para Optimistic Locking (Entrega 2).
    /// </summary>
    public class SeatMapping : IEntityTypeConfiguration<Seat>
    {
        public void Configure(EntityTypeBuilder<Seat> builder)
        {
            builder.ToTable("Seats");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.RowIdentifier)
                .IsRequired()
                .HasMaxLength(5);

            builder.Property(s => s.SeatNumber)
                .IsRequired();

            builder.Property(s => s.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasDefaultValue(SeatStatus.Available);

            // Token de concurrencia para Optimistic Locking
            builder.Property(s => s.Version)
                .IsConcurrencyToken();

            builder.HasOne(s => s.Sector)
                .WithMany(sec => sec.Seats)
                .HasForeignKey(s => s.SectorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}