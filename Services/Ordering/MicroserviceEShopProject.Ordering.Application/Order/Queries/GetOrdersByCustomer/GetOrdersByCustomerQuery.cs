using MicroserviceEShopProject.BuildingBlocks.CQRS;
using MicroserviceEShopProject.Ordering.Application.Dtos;

namespace MicroserviceEShopProject.Ordering.Application.Order.Queries.GetOrdersByCustomer
{
    public record GetOrdersByCustomerQuery(Guid Id) : IQuery<GetOrdersByCustomerResult>;
    public record GetOrdersByCustomerResult(IEnumerable<OrderDto> Orders);
    
}
