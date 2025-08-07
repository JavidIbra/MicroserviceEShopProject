using FluentValidation;
using MicroserviceEShopProject.BuildingBlocks.CQRS;

namespace MicroserviceEShopProject.Ordering.Application.Order.Commands.DeleteOrder
{
    public record DeleteOrderCommand(Guid OrderId): ICommand<DeleteOrderResult>;
    public record DeleteOrderResult(bool IsSuccess);

    public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderCommandValidator()
        {
            RuleFor(x => x.OrderId).NotNull().WithMessage("OrderId is requied");
        }
    }
}
