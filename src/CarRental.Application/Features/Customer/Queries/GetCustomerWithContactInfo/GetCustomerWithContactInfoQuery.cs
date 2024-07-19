using MediatR;

namespace CarRental.Application.Features.Customer.Queries.GetCustomerWithContactInfo
{
    public class GetCustomerWithContactInfoQuery : IRequest<CustomerWithContactInfoResponse>
    {
        public GetCustomerWithContactInfoQuery(string personalIdentityNumber)
        {
            PersonalIdentityNumber = personalIdentityNumber;
        }
        
        public string PersonalIdentityNumber { get; init; }
    }
}