using MicroserviceEShopProject.Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceEShopProject.Discount.Grpc.Data
{
    public class DiscountContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Coupon> Coupons { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>()
                .HasData
                (
                   new Coupon { Id = 1 , ProductName = "Iphone x" , Description = "Iphone Discount", Amount = 3},
                   new Coupon { Id = 2 , ProductName = "Iphone 13" , Description = "Iphone 13 Discount", Amount = 5}
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
