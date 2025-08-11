using MassTransit;
using Microsoft.FeatureManagement;

namespace MicroserviceEShopProject.Ordering.Application.Order.EventHandlers.Domain
{
    public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger, IFeatureManager featureManager, IPublishEndpoint publishEndpoint)
        : INotificationHandler<OrderCreatedEvent>
    {
        public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain event handled: {DomainEvent}", domainEvent.GetType().Name);

            if (await featureManager.IsEnabledAsync("OrderFullFilment"))
            {
                var orderCreatedIntegrationEvent = domainEvent.Order.ToOrderDto();
                await publishEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
            }

        }
    }
}
