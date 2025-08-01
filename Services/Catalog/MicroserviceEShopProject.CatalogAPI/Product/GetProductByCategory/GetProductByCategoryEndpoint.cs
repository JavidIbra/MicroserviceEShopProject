﻿namespace MicroserviceEShopProject.CatalogAPI.Product.GetProductByCategory
{
    public record GetProductByCategoryResponse(IEnumerable<Models.Product> Products);

    public class GetProductByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
            {
                var result = await sender.Send(new GetProductByCategoryQuery(category));

                var response = result.Adapt<GetProductByCategoryResponse>();

                return Results.Ok(response);

            }).WithName("GetProductByCategory")
                .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Get Product By Category")
                .WithDescription("Get Product By Category");
        }
    }
}
