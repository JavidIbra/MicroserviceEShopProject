using MicroserviceEShopProject.BuildingBlocks.CQRS;
using MicroserviceEShopProject.Ordering.Application.Dtos;

namespace MicroserviceEShopProject.Ordering.Application.Order.Queries.GetOrderByName
{
    public record GetOrderByNameQuery(string Name) : IQuery<GetOrderByNameResult>;
    public record GetOrderByNameResult(IEnumerable<OrderDto> Orders);

    

}
