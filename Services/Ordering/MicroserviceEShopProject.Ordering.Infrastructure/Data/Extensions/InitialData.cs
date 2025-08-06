namespace MicroserviceEShopProject.Ordering.Infrastructure.Data.Extensions
{
    internal class InitialData
    {
        public static IEnumerable<Customer> Customers =>
        [
            Customer.Create(CustomerId.Of(new Guid("74f4db9a-b7cd-47c1-810e-73520806b91e")), "john" ,"john@gmail.com"),
            Customer.Create(CustomerId.Of(new Guid("03523e29-4b9a-4652-97e8-cdab38dfdc3e")), "david" , "david@gmail.com")
        ];

        public static IEnumerable<Product> Products =>
        [
            Product.Create(ProductId.Of(new Guid("209f26da-4a31-43b8-ab9e-cc66a999dfe8")), "Iphone x" ,400),
            Product.Create(ProductId.Of(new Guid("b70fe5cc-8877-4465-900a-3dd663ffc83d")), "Samsung10" , 200),
            Product.Create(ProductId.Of(new Guid("958488e1-ba3d-4b28-b774-91603efe0e83")), "Huawei Plus" , 500),
            Product.Create(ProductId.Of(new Guid("8c55582b-772d-4897-b80a-76bc8b9172f6")), "Xiamo Mi" , 800),
        ];

        public static IEnumerable<Order> OrdersWithItems
        {
            get
            {
                var address1 = Address.Of("Mehmet", "Ozkaya", "mehmet@mail.ru", "Street1", "Turkey", "Antalya", 345);
                var address2 = Address.Of("John", "Doe", "mehmet@mail.ru", "Street2", "Sudan", "Makaka", 565);

                var payment1 = Payment.Of("Mehmet", "5555444433339898", "12/27", "355", 1);
                var payment2 = Payment.Of("John", "Ozkaya", "12/28", "488", 2);

                var order1 = Order.Create(
                    OrderId.Of(Guid.NewGuid()),
                    CustomerId.Of(new Guid("74f4db9a-b7cd-47c1-810e-73520806b91e")),
                    OrderName.Of("ord_1"),
                    shippingAddress: address1,
                    billingAddress: address1,
                    payment1);

                order1.Add(ProductId.Of(new Guid("209f26da-4a31-43b8-ab9e-cc66a999dfe8")), 2, 500);
                order1.Add(ProductId.Of(new Guid("b70fe5cc-8877-4465-900a-3dd663ffc83d")), 1, 300);

                var order2 = Order.Create(
                       OrderId.Of(Guid.NewGuid()),
                       CustomerId.Of(new Guid("03523e29-4b9a-4652-97e8-cdab38dfdc3e")),
                       OrderName.Of("ord_2"),
                       shippingAddress: address2,
                       billingAddress: address2,
                       payment2);

                order2.Add(ProductId.Of(new Guid("958488e1-ba3d-4b28-b774-91603efe0e83")), 3, 700);
                order2.Add(ProductId.Of(new Guid("8c55582b-772d-4897-b80a-76bc8b9172f6")), 2, 400);

                return [order1, order2];
            }
        }
    }
}
