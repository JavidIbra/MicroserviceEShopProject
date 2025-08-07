using MicroserviceEShopProject.BuildingBlocks.CQRS;
using MicroserviceEShopProject.Ordering.Application.Data;
using MicroserviceEShopProject.Ordering.Application.Dtos;

namespace MicroserviceEShopProject.Ordering.Application.Order.Commands.UpdateOrder
{
    public class UpdateOrderHandler(IAppDbContext appDbContext)
        : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
    {
        public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            var orderId = OrderId.Of(command.Order.Id);

            var order = await appDbContext.Orders.FindAsync([orderId], cancellationToken)
                ?? throw new OrderNotFoundException(command.Order.Id);

            UpdateOrderWithNewValues(order, command.Order);

            appDbContext.Orders.Update(order);
            await appDbContext.SaveChangesAsync(cancellationToken);

            return new UpdateOrderResult(true);
        }

        public void UpdateOrderWithNewValues(Domain.Models.Order order, OrderDto dto)
        {
            var shippingAddress = Address.Of(dto.ShippingAddress.FirstName, dto.ShippingAddress.LastName, dto.ShippingAddress.EmailAddress, dto.ShippingAddress.AddressLine, dto.ShippingAddress.Country, dto.ShippingAddress.State, dto.ShippingAddress.ZipCode);
            var billingAddress = Address.Of(dto.BillingAddress.FirstName, dto.BillingAddress.LastName, dto.BillingAddress.EmailAddress, dto.BillingAddress.AddressLine, dto.BillingAddress.Country, dto.BillingAddress.State, dto.BillingAddress.ZipCode);
            var updatedPayment = Payment.Of(dto.Payment.CardName, dto.Payment.CardNumber, dto.Payment.Expiration, dto.Payment.Cvv, dto.Payment.PaymentMethod);

            order.Update(
                orderName: OrderName.Of(dto.OrderName),
                shippingAddress: shippingAddress,
                billingAddress: billingAddress,
                payment: updatedPayment,
                orderStatus: dto.OrderStatus
                );
        }
    }
}
