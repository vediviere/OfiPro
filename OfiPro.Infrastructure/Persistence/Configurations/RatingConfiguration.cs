using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OfiPro.Domain.Entities;

namespace OfiPro.Infrastructure.Persistence.Configurations;

public class RatingConfiguration : IEntityTypeConfiguration<Rating>
{
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Score)
            .IsRequired();

        builder.Property(x => x.Comment)
            .HasMaxLength(1000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.DeletedAt)
            .IsRequired(false);

        builder.HasOne(x => x.Contract)
            .WithMany()
            .HasForeignKey(x => x.ContractId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.RaterUser)
            .WithMany()
            .HasForeignKey(x => x.RaterUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.RatedUser)
            .WithMany()
            .HasForeignKey(x => x.RatedUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new
        {
            x.ContractId,
            x.RaterUserId,
            x.RatedUserId
        })
        .IsUnique()
        .HasFilter("[DeletedAt] IS NULL");

        builder.ToTable(x =>
        {
            x.HasCheckConstraint("CK_Ratings_Score_Range", "[Score] >= 1 AND [Score] <= 5");
        });
    }
}