using Core.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Identity.Config
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(p => p.DisplayName).IsRequired();
            builder.Property(p => p.Gender).IsRequired();
            builder.Property(p => p.Weight).HasColumnType("decimal(3,2)");
            builder.Property(p => p.Height).HasColumnType("decimal(3,2)");
            builder.Property(p => p.DateOfBirth).HasColumnType("datetime");
            builder.Property(p => p.ActivityCost).HasColumnType("decimal(2,2)");
            builder.Property(p => p.DailyCalories).HasColumnType("decimal(4,2)");
            builder.Property(p => p.DailyProteins).HasColumnType("decimal(3,2)");
            builder.Property(p => p.DailyCarbohydrates).HasColumnType("decimal(3,2)");
            builder.Property(p => p.DailyFats).HasColumnType("decimal(3,2)");
        }
    }
}