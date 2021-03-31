using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class MealConfiguration : IEntityTypeConfiguration<Meal>
    {
        public void Configure(EntityTypeBuilder<Meal> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Description).IsRequired();
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            builder.Property(p => p.Calories).HasColumnType("decimal(3,2)");
            builder.Property(p => p.Proteins).HasColumnType("decimal(3,2)");
            builder.Property(p => p.Carbohydrates).HasColumnType("decimal(3,2)");
            builder.Property(p => p.Fats).HasColumnType("decimal(3,2)");
            builder.Property(p => p.PictureUrl).IsRequired();
            builder.HasOne(b => b.MealType).WithMany()
                .HasForeignKey(p => p.MealTypeId);
        }
    }
}