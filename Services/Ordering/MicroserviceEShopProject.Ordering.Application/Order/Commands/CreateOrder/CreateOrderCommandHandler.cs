using MicroserviceEShopProject.BuildingBlocks.CQRS;
using MicroserviceEShopProject.Ordering.Application.Data;
using MicroserviceEShopProject.Ordering.Application.Dtos;

namespace MicroserviceEShopProject.Ordering.Application.Order.Commands.CreateOrder
{
    public class CreateOrderCommandHandler(IAppDbContext context)
        : ICommandHandler<CreateOrderCommand, CreateOrderResult>
    {
        public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var order = CreateNewOrder(command.Order);

            context.Orders.Add(order);
            await context.SaveChangesAsync(cancellationToken);

            return new CreateOrderResult(order.Id.Value);
        }

        private Domain.Models.Order CreateNewOrder(OrderDto dto)
        {
            var shippingAddress = Address.Of(dto.ShippingAddress.FirstName, dto.ShippingAddress.LastName, dto.ShippingAddress.EmailAddress, dto.ShippingAddress.AddressLine, dto.ShippingAddress.Country, dto.ShippingAddress.State, dto.ShippingAddress.ZipCode);
            var billingAddress = Address.Of(dto.BillingAddress.FirstName, dto.BillingAddress.LastName, dto.BillingAddress.EmailAddress, dto.BillingAddress.AddressLine, dto.BillingAddress.Country, dto.BillingAddress.State, dto.BillingAddress.ZipCode);

            var newOrder = Domain.Models.Order.Create
                (
                id: OrderId.Of(Guid.NewGuid()),
                customerId: CustomerId.Of(dto.CustomerId),
                orderName: OrderName.Of(dto.OrderName),
                shippingAddress: shippingAddress,
                billingAddress: billingAddress,
                payment: Payment.Of(dto.Payment.CardName, dto.Payment.CardNumber, dto.Payment.Expiration, dto.Payment.Cvv, dto.Payment.PaymentMethod)
                );

            foreach (var item in dto.OrderItems)
            {
                newOrder.Add(ProductId.Of(item.ProductId), item.Quantity, item.Price);
            }

            return newOrder;
        }
    }
}
