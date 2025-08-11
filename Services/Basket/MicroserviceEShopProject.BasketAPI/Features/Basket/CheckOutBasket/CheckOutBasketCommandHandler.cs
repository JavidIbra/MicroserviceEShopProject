using MassTransit;
using MicroserviceEShopProject.BuildingBlocks.Messaging.Events;

namespace MicroserviceEShopProject.BasketAPI.Features.Basket.CheckOutBasket
{
    public record CheckOutBasketCommand(BasketCheckOutDto BasketCheckOutDto) : ICommand<CheckOutBasketResult>;
    public record CheckOutBasketResult(bool IsSuccess);

    public class CheckOutBasketCommandValidator : AbstractValidator<CheckOutBasketCommand>
    {
        public CheckOutBasketCommandValidator()
        {
            RuleFor(x => x.BasketCheckOutDto).NotNull().WithMessage(" BasketCheckOutDto cant be null");
            RuleFor(x => x.BasketCheckOutDto.UserName).NotEmpty().WithMessage("UserName is required");
        }
    }

    public class CheckOutBasketCommandHandler(IBasketRepository basketRepository, IPublishEndpoint publishEndpoint)
        : ICommandHandler<CheckOutBasketCommand, CheckOutBasketResult>
    {
        public async Task<CheckOutBasketResult> Handle(CheckOutBasketCommand command, CancellationToken cancellationToken)
        {
            var basket = await basketRepository.GetBasket(command.BasketCheckOutDto.UserName, cancellationToken);

            if (basket == null)
                return new CheckOutBasketResult(false);

            var eventMessage = command.BasketCheckOutDto.Adapt<BasketCheckOutEvent>();
            eventMessage.TotalPrice = basket.TotalPrice;

            await publishEndpoint.Publish(eventMessage, cancellationToken);
            await basketRepository.DeleteBasket(command.BasketCheckOutDto.UserName, cancellationToken);

            return new CheckOutBasketResult(true);
        }
    }
}
