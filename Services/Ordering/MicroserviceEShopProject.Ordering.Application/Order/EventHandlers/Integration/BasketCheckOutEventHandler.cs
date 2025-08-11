using MassTransit;
using MicroserviceEShopProject.BuildingBlocks.Messaging.Events;
using MicroserviceEShopProject.Ordering.Application.Dtos;
using MicroserviceEShopProject.Ordering.Application.Order.Commands.CreateOrder;
using MicroserviceEShopProject.Ordering.Domain.Enums;

namespace MicroserviceEShopProject.Ordering.Application.Order.EventHandlers.Integration
{
    public class BasketCheckOutEventHandler(ISender sender, ILogger<BasketCheckOutEventHandler> logger)
        : IConsumer<BasketCheckOutEvent>
    {
        public async Task Consume(ConsumeContext<BasketCheckOutEvent> context)
        {

            logger.LogInformation("Integration Event handled {IntegrationEvent}", context.Message.GetType().Name);

            var command = MapToCreateOrderCommand(context.Message);
            await sender.Send(command);

        }

        private CreateOrderCommand MapToCreateOrderCommand(BasketCheckOutEvent message)
        {
            // create full order with incoming message
            var addressDto = new AddressDto(message.FirstName, message.LastName, message.EmailAddress, message.AddressLine, message.Country, message.State, message.ZipCode);
            var paymentDto = new PaymentDto(message.CardName, message.CardNumber, message.Expiration, message.Cvv, message.PaymentMethod);
            var orderId = Guid.NewGuid();

            var orderDto = new OrderDto(
                Id: orderId,
                CustomerId: message.CustomerId,
                OrderName: message.UserName,
                BillingAddress: addressDto,
                ShippingAddress: addressDto,
                Payment: paymentDto,
                OrderStatus: OrderStatus.Pending,

                OrderItems: [
                    new OrderItemDto(OrderId:orderId , new Guid("209f26da-4a31-43b8-ab9e-cc66a999dfe8"),2,500),
                    new OrderItemDto(OrderId:orderId , new Guid("b70fe5cc-8877-4465-900a-3dd663ffc83d"),1,400)
                    ]
            );

            return new CreateOrderCommand(orderDto);
        }
    }
}
