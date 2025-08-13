namespace MicroserviceEShopProject.Web.Pages
{
    public class ConfirmationModel : PageModel
    {
        public string Message { get; set; } = default!;

        public void OnGetContact()
        {
            Message = "your email was sent";
        }

        public void OnGetOrderSubmitted()
        {
            Message = "your order submitted successfully";
        }
    }
}
