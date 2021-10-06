using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AlgoFit.Data.Models;

namespace AlgoFit.Data.Configurations
{
    public class DietCategoryConfiguration : IEntityTypeConfiguration<DietCategory>
    {
        public void Configure(EntityTypeBuilder<DietCategory> builder)
        {   
            builder.HasKey(rs => new { rs.DietId, rs.CategoryId });
            builder.HasOne(dc => dc.Diet)
                .WithMany(d => d.Categories)
                .HasForeignKey(dc => dc.DietId);

            builder.HasOne(dc => dc.Category)
                .WithMany();
        }
    }
}