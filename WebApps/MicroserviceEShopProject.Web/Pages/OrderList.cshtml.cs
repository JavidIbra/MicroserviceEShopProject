namespace MicroserviceEShopProject.Web.Pages
{
    public class OrderListModel(IOrderingService orderingService, ILogger<OrderListModel> logger)
        : PageModel
    {
        public IEnumerable<OrderModel> Orders { get; set; } = default!;

        public async Task<IActionResult>  OnGetAsync()
        {
            var customerId = new Guid("74f4db9a-b7cd-47c1-810e-73520806b91e");

            var response = await orderingService.GetOrdersByCustomer(customerId);

            Orders = response.Orders;

            return Page();
        }
    }
}
