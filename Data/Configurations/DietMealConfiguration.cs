using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AlgoFit.Data.Models;

namespace AlgoFit.Data.Configurations
{
    public class DietMealConfiguration : IEntityTypeConfiguration<DietMeal>
    {
        public void Configure(EntityTypeBuilder<DietMeal> builder)
        {   
            builder.HasOne(dm => dm.Diet)
                .WithMany(d => d.Meals)
                .HasForeignKey(dm => dm.DietId);

            builder.HasOne(dm => dm.Meal)
                .WithMany();
        }
    }
}