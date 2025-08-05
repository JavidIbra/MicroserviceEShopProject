using MicroserviceEShopProject.Ordering.Domain.Models;
using MicroserviceEShopProject.Ordering.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroserviceEShopProject.Ordering.Infrastructure.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
            .HasConversion(customerId => customerId.Value, dbId => OrderItemId.Of(dbId));

            builder.HasOne<Product>().WithMany().HasForeignKey(c => c.ProductId);

            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.Price).IsRequired();
        }
    }
}
