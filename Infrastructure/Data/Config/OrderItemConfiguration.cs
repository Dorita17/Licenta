using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(o => o.MealItemOrdered, a => 
            {
                a.WithOwner();
            });
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            builder.Property(p => p.Calories).HasColumnType("decimal(3,2)");
            builder.Property(p => p.Proteins).HasColumnType("decimal(3,2)");
            builder.Property(p => p.Carbohydrates).HasColumnType("decimal(3,2)");
            builder.Property(p => p.Fats).HasColumnType("decimal(3,2)");
        }
    }
}