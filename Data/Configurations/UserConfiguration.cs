using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AlgoFit.Data.Models;

namespace AlgoFit.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(u => u.Email).IsUnique();
            
            builder.HasOne(u => u.UserCredential)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}