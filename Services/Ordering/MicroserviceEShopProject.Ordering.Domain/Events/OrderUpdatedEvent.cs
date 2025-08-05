using MicroserviceEShopProject.Ordering.Domain.Models;

namespace MicroserviceEShopProject.Ordering.Domain.Events
{
    public record OrderUpdatedEvent(Order Order) : IDomainEvent;
}
