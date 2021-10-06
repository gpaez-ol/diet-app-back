using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AlgoFit.Data.Models;

namespace AlgoFit.Data.Configurations
{
    public class MealIngredientConfiguration : IEntityTypeConfiguration<MealIngredient>
    {
        public void Configure(EntityTypeBuilder<MealIngredient> builder)
        {   
            builder.HasKey(mi => new { mi.MealId, mi.IngredientId });
            builder.HasOne(mi => mi.Meal)
                .WithMany(m => m.Ingredients)
                .HasForeignKey(mi => mi.MealId);

            builder.HasOne(mi => mi.Ingredient)
                .WithMany();
        }
    }
}