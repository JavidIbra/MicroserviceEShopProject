using MicroserviceEShopProject.BuildingBlocks.CQRS;
using MicroserviceEShopProject.Ordering.Application.Data;

namespace MicroserviceEShopProject.Ordering.Application.Order.Queries.GetOrdersByCustomer
{
    public class GetOrdersByCustomerQueryHandler(IAppDbContext dbContext)
        : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
    {
        public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery query, CancellationToken cancellationToken)
        {
            var orders = await dbContext.Orders.
                Include(x => x.OrderItems)
                .AsNoTracking()
                .Where(x => x.CustomerId == CustomerId.Of(query.Id))
                .OrderBy(x => x.OrderName)
                .ToListAsync(cancellationToken);

            return new GetOrdersByCustomerResult(orders.ProjectToOrderDto());
        }
    }
}
