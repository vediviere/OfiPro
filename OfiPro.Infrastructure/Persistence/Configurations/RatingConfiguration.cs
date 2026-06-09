using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OfiPro.Domain.Entities;

namespace OfiPro.Infrastructure.Persistence.Configurations;

public class RatingConfiguration : IEntityTypeConfiguration<Rating>
{
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Project)
            .WithMany()
            .HasForeignKey(x => x.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.EvaluatorUser)
            .WithMany()
            .HasForeignKey(x => x.EvaluatorUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.EvaluatedUser)
            .WithMany()
            .HasForeignKey(x => x.EvaluatedUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.Quality)
            .IsRequired();

        builder.Property(x => x.Punctuality)
            .IsRequired();

        builder.Property(x => x.Communication)
            .IsRequired();

        builder.Property(x => x.Professionalism)
            .IsRequired();

        builder.Property(x => x.CostBenefit)
            .IsRequired();

        builder.Property(x => x.Comment)
            .HasMaxLength(1500);
    }
}