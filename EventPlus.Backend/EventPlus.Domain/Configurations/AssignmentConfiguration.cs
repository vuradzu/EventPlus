using EventPlus.Domain.Entities;
using EventPlus.Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventPlus.Domain.Configurations;

public class AssignmentConfiguration : IEntityTypeConfiguration<Assignment>
{
    public void Configure(EntityTypeBuilder<Assignment> builder)
    {
        builder.Property(e => e.Title).AsSmallText();
        builder.Property(e => e.Description).AsLargeText();
        
        builder.HasOne(a => a.Creator)
            .WithMany()
            .HasForeignKey(a => a.CreatorId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(a => a.Assignee)
            .WithMany()
            .HasForeignKey(a => a.AssigneId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}