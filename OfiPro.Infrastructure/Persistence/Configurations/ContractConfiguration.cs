using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OfiPro.Domain.Entities;

namespace OfiPro.Infrastructure.Persistence.Configurations;

public class ContractConfiguration : IEntityTypeConfiguration<Contract>
{
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.AgreedPrice)
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.EstimatedTime)
            .HasMaxLength(100);

        builder.HasOne(x => x.Proposal)
            .WithMany()
            .HasForeignKey(x => x.ProposalId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ProjectRequirement)
            .WithMany()
            .HasForeignKey(x => x.ProjectRequirementId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ClientUser)
            .WithMany()
            .HasForeignKey(x => x.ClientUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ContractorUser)
            .WithMany()
            .HasForeignKey(x => x.ContractorUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}