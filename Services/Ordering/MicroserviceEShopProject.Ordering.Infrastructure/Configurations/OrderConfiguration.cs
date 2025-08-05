using MicroserviceEShopProject.Ordering.Domain.Enums;
using MicroserviceEShopProject.Ordering.Domain.Models;
using MicroserviceEShopProject.Ordering.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroserviceEShopProject.Ordering.Infrastructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
            .HasConversion(customerId => customerId.Value, dbId => OrderId.Of(dbId));

            builder.HasOne<Customer>().WithMany().HasForeignKey(c => c.CustomerId).IsRequired();

            builder.HasMany(c => c.OrderItems).WithOne().HasForeignKey(c => c.OrderId);

            builder.ComplexProperty(o => o.OrderName, nameBuilder =>
            {
                nameBuilder.Property(n => n.Value)
                           .HasColumnName(nameof(Order.OrderName))
                           .HasMaxLength(100)
                           .IsRequired();
            });

            builder.ComplexProperty(o => o.ShippingAddress, nameBuilder =>
            {
                nameBuilder.Property(n => n.FirstName)
                           .HasMaxLength(50)
                           .IsRequired();

                nameBuilder.Property(n => n.LastName)
                          .HasMaxLength(50)
                           .IsRequired();

                nameBuilder.Property(n => n.EmailAddress)
                          .HasMaxLength(50);

                nameBuilder.Property(n => n.AddressLine)
                          .HasMaxLength(50)
                          .IsRequired();

                nameBuilder.Property(n => n.Country)
                           .HasMaxLength(50);

                nameBuilder.Property(n => n.State)
                            .HasMaxLength(50);

                nameBuilder.Property(n => n.ZipCode)
                            .HasMaxLength(5)
                          .IsRequired();

            });

            builder.ComplexProperty(o => o.BillingAddress, nameBuilder =>
            {
                nameBuilder.Property(n => n.FirstName)
                           .HasMaxLength(50)
                           .IsRequired();

                nameBuilder.Property(n => n.LastName)
                          .HasMaxLength(50)
                           .IsRequired();

                nameBuilder.Property(n => n.EmailAddress)
                          .HasMaxLength(50);

                nameBuilder.Property(n => n.AddressLine)
                          .HasMaxLength(50)
                          .IsRequired();

                nameBuilder.Property(n => n.Country)
                           .HasMaxLength(50);

                nameBuilder.Property(n => n.State)
                            .HasMaxLength(50);

                nameBuilder.Property(n => n.ZipCode)
                            .HasMaxLength(5)
                          .IsRequired();

            });

            builder.ComplexProperty(o => o.Payment, nameBuilder =>
            {
                nameBuilder.Property(n => n.CardName)
                           .HasMaxLength(50);

                nameBuilder.Property(n => n.CardNumber)
                          .HasMaxLength(24)
                           .IsRequired();

                nameBuilder.Property(n => n.Expiration)
                          .HasMaxLength(10);

                nameBuilder.Property(n => n.CVV)
                          .HasMaxLength(3)
                          .IsRequired();

                nameBuilder.Property(n => n.PeymnatMethod);
            });

            builder.Property(o => o.Status)
                .HasDefaultValue(OrderStatus.Draft)
                .HasConversion(x => x.ToString(),
                dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));

            builder.Property(n => n.TotalPrice);
        }
    }
}
