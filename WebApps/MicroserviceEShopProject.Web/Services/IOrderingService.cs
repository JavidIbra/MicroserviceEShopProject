namespace MicroserviceEShopProject.Web.Services
{
    public interface IOrderingService
    {
        [Get("/ordering-service/orders?pageNumber={pageNumber}&pageSize={pageSize}")]
        Task<GetBasketResponse> GetOrders(int? pageNumber = 1, int? pageSize = 10);

        [Get("/ordering-service/orders/{orderName}")]
        Task<GetOrdersByNameResponse> GetOrdersByName(string orderName);

        [Get("/ordering-service/orders/customer/{customerId}")]
        Task<GetOrdersByCustomerResponse> GetOrdersByCustomer(Guid customerId);
    }
}
