using MicroserviceEShopProject.Ordering.Domain.Models;

namespace MicroserviceEShopProject.Ordering.Domain.Events
{
    public record OrderCreatedEvent(Order Order) : IDomainEvent;
}
