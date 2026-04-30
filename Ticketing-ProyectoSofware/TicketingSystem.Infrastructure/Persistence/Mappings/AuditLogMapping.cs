using TicketingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TicketingSystem.Infrastructure.Persistence.Mappings
{
    public class AuditLogMapping : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.ToTable("AuditLog");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Action)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(a => a.EntityType)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.EntityId)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.Details)
                .HasMaxLength(1000);

            builder.Property(a => a.CreatedAt)
                .IsRequired();
        }
    }
}