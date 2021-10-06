using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AlgoFit.Data.Models;

namespace AlgoFit.Data.Configurations
{
    public class UserDietConfiguration : IEntityTypeConfiguration<UserDiet>
    {
        public void Configure(EntityTypeBuilder<UserDiet> builder)
        {   
            builder.HasKey(rs => new { rs.DietId, rs.UserId });
            builder.HasOne(dc => dc.User)
                .WithMany(d => d.Diets)
                .HasForeignKey(dc => dc.DietId);

            builder.HasOne(dc => dc.Diet)
                .WithMany();
        }
    }
}