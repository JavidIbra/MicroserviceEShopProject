using MicroserviceEShopProject.BuildingBlocks.CQRS;
using MicroserviceEShopProject.BuildingBlocks.Pagination;
using MicroserviceEShopProject.Ordering.Application.Dtos;

namespace MicroserviceEShopProject.Ordering.Application.Order.Queries.GetOrders
{
    public record GetOrdersQuery(PaginationRequest PaginationRequet) : IQuery<GetOrdersResult>;
    public record GetOrdersResult(PaginatedResult<OrderDto> Orders);

}
