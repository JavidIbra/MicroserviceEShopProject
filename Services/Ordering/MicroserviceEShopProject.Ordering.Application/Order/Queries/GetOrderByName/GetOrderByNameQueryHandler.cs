using MicroserviceEShopProject.BuildingBlocks.CQRS;
using MicroserviceEShopProject.Ordering.Application.Data;
using MicroserviceEShopProject.Ordering.Application.Extensions;

namespace MicroserviceEShopProject.Ordering.Application.Order.Queries.GetOrderByName
{
    public class GetOrderByNameQueryHandler(IAppDbContext appDbContext)
        : IQueryHandler<GetOrderByNameQuery, GetOrderByNameResult>
    {
        public async Task<GetOrderByNameResult> Handle(GetOrderByNameQuery query, CancellationToken cancellationToken)
        {
            var orders = await appDbContext.Orders
                .Include(x => x.OrderItems)
                .AsNoTracking()
                .Where(x => x.OrderName.Value.Contains(query.Name)).OrderBy(x => x.OrderName)
                .ToListAsync(cancellationToken);

            return new GetOrderByNameResult(orders.ProjectToOrderDto());
        }
    }
}
