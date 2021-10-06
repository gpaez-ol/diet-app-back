using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AlgoFit.Data.Models;

namespace AlgoFit.Data.Configurations
{
    public class BiometricConfiguration : IEntityTypeConfiguration<Biometric>
    {
        public void Configure(EntityTypeBuilder<Biometric> builder)
        {   
            builder.HasOne(b => b.User)
                .WithMany();
        }
    }
}