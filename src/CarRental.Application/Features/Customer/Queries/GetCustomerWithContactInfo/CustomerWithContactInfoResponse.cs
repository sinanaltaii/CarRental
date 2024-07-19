namespace CarRental.Application.Features.Customer.Queries.GetCustomerWithContactInfo
{
    public record CustomerWithContactInfoResponse
    {
        public CustomerWithContactInfoResponse(CustomerResponse customerResponse,
            CustomerContactInfoResponse customerContactInfoResponse)
        {
            CustomerResponse = customerResponse;
            CustomerContactInfoResponse = customerContactInfoResponse;
        }
        public CustomerResponse? CustomerResponse { get; set; }

        public CustomerContactInfoResponse? CustomerContactInfoResponse { get; set; }
    }
}
