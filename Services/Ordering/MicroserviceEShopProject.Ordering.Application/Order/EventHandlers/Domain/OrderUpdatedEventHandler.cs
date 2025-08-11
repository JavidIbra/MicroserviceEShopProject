namespace MicroserviceEShopProject.Ordering.Application.Order.EventHandlers.Domain
{
    public class OrderUpdatedEventHandler(ILogger<OrderUpdatedEventHandler> logger)
        : INotificationHandler<OrderUpdatedEvent>
    {
        public Task Handle(OrderUpdatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain event handled: {DomainEvent}", notification.GetType().Name);

            //notification.Order
            return Task.CompletedTask;
        }
    }
}
