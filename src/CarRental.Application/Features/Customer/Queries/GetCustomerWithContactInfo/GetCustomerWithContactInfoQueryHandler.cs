using CarRental.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Application.Features.Customer.Queries.GetCustomerWithContactInfo
{
    public class GetCustomerWithContactInfoQueryHandler : IRequestHandler<GetCustomerWithContactInfoQuery, CustomerWithContactInfoResponse>
    {
        private readonly CarRentalContext _carRentalContext;

        public GetCustomerWithContactInfoQueryHandler(CarRentalContext carRentalContext)
        {
            _carRentalContext = carRentalContext;
        }

        public async Task<CustomerWithContactInfoResponse> Handle(GetCustomerWithContactInfoQuery request, CancellationToken cancellationToken)
        {
            var customerWithContactInfo = await _carRentalContext.Customers
                .Include(p => p.ContactInfo)
                .FirstOrDefaultAsync(p => p.PersonalIdentityNumber == request.PersonalIdentityNumber, cancellationToken);

            if (customerWithContactInfo is null)
            {
                return new NullCustomerWithContactInfoResponse();
            }

            var customerResponse = new CustomerResponse(customerWithContactInfo.Id, customerWithContactInfo.FirstName, customerWithContactInfo.LastName, customerWithContactInfo.PersonalIdentityNumber);
            var contactInfoResponse =
                ContactInfoResponse(customerWithContactInfo);

            return new CustomerWithContactInfoResponse(customerResponse, contactInfoResponse);
        }

        private static CustomerContactInfoResponse ContactInfoResponse(Domains.Customer customerWithContactInfo)
        {
            return customerWithContactInfo.ContactInfo != null
                ? new CustomerContactInfoResponse(
                    customerWithContactInfo.ContactInfo.StreetAddress,
                    customerWithContactInfo.ContactInfo.StreetAddressLine2 ?? string.Empty,
                    customerWithContactInfo.ContactInfo.PostalCode,
                    customerWithContactInfo.ContactInfo.City,
                    customerWithContactInfo.ContactInfo.PhoneNumber,
                    customerWithContactInfo.ContactInfo.AlternatePhoneNumber ?? string.Empty,
                    customerWithContactInfo.ContactInfo.EmailAddress!)
                : new NullCustomerContactInfoResponse();
        }
    }

    public record NullCustomerResponse : CustomerResponse
    {
        public NullCustomerResponse() : base(0, string.Empty, string.Empty, string.Empty)
        {
        }
    }

    public record NullCustomerContactInfoResponse : CustomerContactInfoResponse
    {
        public NullCustomerContactInfoResponse() : base(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty)
        {
        }
    }

    public record NullCustomerWithContactInfoResponse : CustomerWithContactInfoResponse
    {
        public NullCustomerWithContactInfoResponse() : base(new NullCustomerResponse(), new NullCustomerContactInfoResponse())
        {
        }
    }
}