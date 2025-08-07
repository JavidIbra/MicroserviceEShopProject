using MicroserviceEShopProject.BuildingBlocks.CQRS;
using MicroserviceEShopProject.Ordering.Application.Data;

namespace MicroserviceEShopProject.Ordering.Application.Order.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler(IAppDbContext appDbContext)
        : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
    {
        public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            var orderId = OrderId.Of(command.OrderId);
            var order = await appDbContext.Orders.FindAsync([orderId], cancellationToken)
                ?? throw new OrderNotFoundException(command.OrderId);

            appDbContext.Orders.Remove(order);

            await appDbContext.SaveChangesAsync(cancellationToken);

            return new DeleteOrderResult(true);
        }
    }
}
