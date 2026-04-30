    using TicketingSystem.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    namespace TicketingSystem.Infrastructure.Persistence.Mappings
    {
        public class UserMapping : IEntityTypeConfiguration<User>
        {
            public void Configure(EntityTypeBuilder<User> builder)
            {
                builder.ToTable("Users");

                builder.HasKey(u => u.Id);

                builder.Property(u => u.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                builder.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(200);

                builder.HasIndex(u => u.Email)
                    .IsUnique();

                builder.Property(u => u.PasswordHash)
                    .IsRequired();
            }
        }
    }