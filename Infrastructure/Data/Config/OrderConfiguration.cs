using System;
using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.DeliverToAddress, a => 
            {
                a.WithOwner();
            });
            builder.Property(p => p.Status)
                .HasConversion(
                    o => o.ToString(),
                    o => (OrderStatus) Enum.Parse(typeof(OrderStatus), o)
                );
            builder.HasMany(p => p.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}