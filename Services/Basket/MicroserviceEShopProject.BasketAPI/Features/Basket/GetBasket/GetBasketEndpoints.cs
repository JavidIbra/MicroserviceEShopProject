﻿namespace MicroserviceEShopProject.BasketAPI.Features.Basket.GetBasket
{
    //public record GetBasketRequest(string UserName);

    public record GetBasketResponse(ShoppingCart Cart);

    public class GetBasketEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
            {
                var result = await sender.Send(new GetBasketQuery(userName));

                var baskets = result.Adapt<GetBasketResponse>();

                return Results.Ok(baskets);

            }).WithName("GetBasket")
                .Produces<GetBasketResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Get Basket")
                .WithDescription("Get Basket");
        }
    }
}
