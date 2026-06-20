using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OfiPro.Domain.Entities;

namespace OfiPro.Infrastructure.Persistence.Configurations;

public class EvidenceConfiguration : IEntityTypeConfiguration<Evidence>
{
    public void Configure(EntityTypeBuilder<Evidence> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(120);

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.FileUrl)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.FileType)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasOne(x => x.Contract)
            .WithMany()
            .HasForeignKey(x => x.ContractId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.UploadedByUser)
            .WithMany()
            .HasForeignKey(x => x.UploadedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.ContractId);

        builder.HasIndex(x => x.UploadedByUserId);
    }
}