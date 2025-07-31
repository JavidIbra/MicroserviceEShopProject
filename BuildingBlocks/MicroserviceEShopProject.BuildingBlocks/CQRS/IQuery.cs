using MediatR;

namespace MicroserviceEShopProject.BuildingBlocks.CQRS
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
        where TResponse : notnull
    {
    }
}
