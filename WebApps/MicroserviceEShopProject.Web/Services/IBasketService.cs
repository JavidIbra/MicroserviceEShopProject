namespace MicroserviceEShopProject.Web.Services
{
    public interface IBasketService
    {
        [Get("/basket-service/basket/{userName}")]
        Task<GetBasketResponse> GetBasket(string userName);

        [Post("/basket-service/basket")]
        Task<GetProductByIdResponse> StoreBasket(StoreBasketRequest request);

        [Delete("/basket-service/basket/{userName}")]
        Task<GetProductByCategoryResponse> DeleteBasket(string userName);

        [Post("/basket-service/basket/checkout")]
        Task<GetProductByIdResponse> CheckOutBasket(CheckOutBasketRequest request);

        public async Task<ShoppingCartModel> LoadUserBasket()
        {
            var userName = "Mehmet";
            ShoppingCartModel basket;

            try
            {
                var getBasketResponse = await GetBasket(userName);

                basket = getBasketResponse.Cart;
            }
            catch (ApiException apiException) when (apiException.StatusCode==System.Net.HttpStatusCode.NotFound)
            {
                basket = new ShoppingCartModel
                {
                    UserName = userName,
                    Items = []
                };
            }

            return basket;
        }
    }
}
