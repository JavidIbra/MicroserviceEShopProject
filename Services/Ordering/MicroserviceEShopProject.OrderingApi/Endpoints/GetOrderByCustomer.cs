namespace MicroserviceEShopProject.OrderingApi.Endpoints
{
    //public record GetOrdersByCustomerNameResponse(Guid Id);
    public record GetOrdersByCustomerIdResponse(IEnumerable<OrderDto> Orders);

    public class GetOrderByCustomer : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/customer/{customerId}", async (Guid customerId, ISender sender) =>
            {
                var result = await sender.Send(new GetOrdersByCustomerQuery(customerId));

                var response = result.Adapt<GetOrdersByCustomerIdResponse>();

                return Results.Ok(response);

            }).WithName("GetOrdersByCustomerId")
            .Produces<GetOrdersByCustomerIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Orders By Customer Id")
            .WithDescription("Get Orders By Customer Id");
        }
    }
}
