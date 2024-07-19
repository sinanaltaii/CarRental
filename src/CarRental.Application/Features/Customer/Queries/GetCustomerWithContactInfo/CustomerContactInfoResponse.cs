namespace CarRental.Application.Features.Customer.Queries.GetCustomerWithContactInfo
{
    public record CustomerContactInfoResponse
    {
        public CustomerContactInfoResponse( 
            string streetAddress, 
            string streetAddressLine2,
            string postalCode,
            string city, 
            string phoneNumber,
            string alternatePhoneNumber,
            string emailAddress)
        {
            StreetAddress = streetAddress;
            StreetAddressLine2 = streetAddressLine2;
            PostalCode = postalCode;
            City = city;
            PhoneNumber = phoneNumber;
            AlternatePhoneNumber = alternatePhoneNumber;
            EmailAddress = emailAddress;
        }

        public string StreetAddress { get; set; } = string.Empty;
        public string? StreetAddressLine2 { get; set; }
        public string PostalCode { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? AlternatePhoneNumber { get; set; } 
        public string? EmailAddress { get; set;} = string.Empty;
    }
}