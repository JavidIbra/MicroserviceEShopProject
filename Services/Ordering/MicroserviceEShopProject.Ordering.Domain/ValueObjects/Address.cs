namespace MicroserviceEShopProject.Ordering.Domain.ValueObjects
{
    public record Address
    {
        protected Address()
        {
        }

        private Address(string firstName, string lastName, string? emailAddress, string addressLine, string country, decimal state, decimal zipCode)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            AddressLine = addressLine;
            Country = country;
            State = state;
            ZipCode = zipCode;
        }

        public string FirstName { get; } = default!;
        public string LastName { get; } = default!;
        public string? EmailAddress { get; } = default!;
        public string AddressLine { get; } = default!;
        public string Country { get; } = default!;
        public decimal State { get; } = default!;
        public decimal ZipCode { get; } = default!;

        public static Address Of(string firstName, string lastName, string? emailAddress, string addressLine, string country, decimal state, decimal zipCode)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(emailAddress);
            ArgumentException.ThrowIfNullOrWhiteSpace(addressLine);

            return new Address(firstName,lastName,emailAddress,addressLine,country,state,zipCode);
        }
    }
}
