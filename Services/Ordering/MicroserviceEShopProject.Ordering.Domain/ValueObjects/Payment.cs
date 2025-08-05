namespace MicroserviceEShopProject.Ordering.Domain.ValueObjects
{
    public record Payment
    {
        protected Payment()
        {
        }
        private Payment(string? cardName, string cardNumber, string expiration, string cVV, int peymnatMethod)
        {
            CardName = cardName;
            CardNumber = cardNumber;
            Expiration = expiration;
            CVV = cVV;
            PeymnatMethod = peymnatMethod;
        }

        public string? CardName { get; } = default!;
        public string CardNumber { get; } = default!;
        public string Expiration { get; } = default!;
        public string CVV { get; } = default!;
        public int PeymnatMethod { get; } = default!;


        public static Payment Of(string cardName, string cardNumber, string expiration, string cVV, int peymnatMethod)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(cardName);
            ArgumentException.ThrowIfNullOrWhiteSpace(cardNumber);
            ArgumentException.ThrowIfNullOrWhiteSpace(cVV);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(cVV.Length, 3);

            return new Payment(cardName, cardNumber, expiration, cVV, peymnatMethod);
        }
    }
}
