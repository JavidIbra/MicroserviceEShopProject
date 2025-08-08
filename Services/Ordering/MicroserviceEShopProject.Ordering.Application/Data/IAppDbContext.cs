namespace MicroserviceEShopProject.Ordering.Application.Data
{
    public interface IAppDbContext
    {
        DbSet<Customer> Customers { get; }
        DbSet<Product> Products { get; }
        DbSet<Domain.Models.Order> Orders { get; }
        DbSet<OrderItem> OrderItems { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
