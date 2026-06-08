using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OfiPro.Domain.Entities;

namespace OfiPro.Infrastructure.Persistence.Configurations;

public class ProposalConfiguration : IEntityTypeConfiguration<Proposal>
{
    public void Configure(EntityTypeBuilder<Proposal> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Project)
            .WithMany()
            .HasForeignKey(x => x.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ContractorUser)
            .WithMany()
            .HasForeignKey(x => x.ContractorUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.Price)
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.EstimatedTime)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.ScopeDescription)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(x => x.Includes)
            .HasMaxLength(1500);

        builder.Property(x => x.DoesNotInclude)
            .HasMaxLength(1500);

        builder.Property(x => x.WarrantyDescription)
            .HasMaxLength(1000);

        builder.Property(x => x.Comment)
            .HasMaxLength(1500);

        builder.Property(x => x.Status)
            .IsRequired();
    }
}
