using MicroserviceEShopProject.BuildingBlocks.CQRS;
using MicroserviceEShopProject.BuildingBlocks.Pagination;
using MicroserviceEShopProject.Ordering.Application.Data;
using MicroserviceEShopProject.Ordering.Application.Dtos;

namespace MicroserviceEShopProject.Ordering.Application.Order.Queries.GetOrders
{
    public class GetOrdersHandler(IAppDbContext dbContext)
        : IQueryHandler<GetOrdersQuery, GetOrdersResult>
    {
        public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
        {
            var pageIndex = query.PaginationRequet.PageIndex;
            var pageSize = query.PaginationRequet.PageSize;

            var totalCount = await dbContext.Orders.LongCountAsync(cancellationToken);

            var orders = await dbContext.Orders.Include(x => x.OrderItems)
                .OrderBy(x => x.OrderName.Value)
                .Skip(pageSize * pageIndex).Take(pageSize).ToListAsync(cancellationToken: cancellationToken);

            return new GetOrdersResult(new PaginatedResult<OrderDto>(pageIndex, pageSize, totalCount, orders.ProjectToOrderDto()));
        }
    }
}
