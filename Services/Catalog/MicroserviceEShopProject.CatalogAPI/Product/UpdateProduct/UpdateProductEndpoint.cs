﻿using MicroserviceEShopProject.CatalogAPI.Product.GetProducts;

namespace MicroserviceEShopProject.CatalogAPI.Product.UpdateProduct
{
    public record UpdateProductRequest(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price);

    public record UpdateProductResponse(bool IsSuccess);

    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {

            app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateProductCommand>();

                var result = sender.Send(command);

                var response = result.Adapt<UpdateProductResponse>();

                return Results.Ok(response);

            }).WithName("UpdateProduct")
                 .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
                 .ProducesProblem(StatusCodes.Status400BadRequest)
                 .ProducesProblem(StatusCodes.Status404NotFound)
                 .WithSummary("Update Products")
                 .WithDescription("Update Products");

        }
    }
}
