using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OfiPro.Domain.Entities;
using OfiPro.Domain.Enums;

namespace OfiPro.Infrastructure.Persistence.Configurations;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Type)
            .IsRequired()
            .HasConversion<int>()
            .HasDefaultValue(NotificationType.General);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(120);

        builder.Property(x => x.Message)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.RelatedEntityType)
            .HasMaxLength(80);

        builder.Property(x => x.IsRead)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.UserId);

        builder.HasIndex(x => new { x.UserId, x.IsRead });

        builder.HasIndex(x => x.CreatedAt);
    }
}