using TicketingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TicketingSystem.Infrastructure.Persistence.Mappings
{
    public class SectorMapping : IEntityTypeConfiguration<Sector>
    {
        public void Configure(EntityTypeBuilder<Sector> builder)
        {
            builder.ToTable("Sectors");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.Price)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.Property(s => s.Capacity)
                .IsRequired();

        }
    }
}
