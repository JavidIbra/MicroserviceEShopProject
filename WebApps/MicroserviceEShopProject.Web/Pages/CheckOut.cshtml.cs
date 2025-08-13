namespace MicroserviceEShopProject.Web.Pages
{
    public class CheckOutModel(IBasketService basketService, ILogger<CheckOutModel> logger) : PageModel
    {
        [BindProperty]
        public BasketCheckOutModel Order { get; set; } = default!;
        public ShoppingCartModel Cart { get; set; } = default!;


        public async Task<IActionResult> OnGetAsync()
        {
            Cart = await basketService.LoadUserBasket();

            return Page();
        }

        public async Task<IActionResult> OnPostCheckOutAsync()
        {
            logger.LogInformation("Check Out clikceed");

            Cart = await basketService.LoadUserBasket();

            if (!ModelState.IsValid)
                return Page();

            Order.CustomerId = new Guid("66b8a1ba-c68c-499b-b308-c5275af67917");
            Order.UserName = Cart.UserName;
            Order.TotalPrice = Cart.TotalPrice;

            await basketService.CheckOutBasket(new CheckOutBasketRequest(Order));
            return RedirectToPage("Confirmation", "OrderSubmitted");

        }
    }
}
