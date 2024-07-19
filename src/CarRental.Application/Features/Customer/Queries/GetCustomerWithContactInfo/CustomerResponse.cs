namespace CarRental.Application.Features.Customer.Queries.GetCustomerWithContactInfo
{
    public record CustomerResponse
    {
        public CustomerResponse(int id, string firstName, string lastName, string personalIdentityNumber)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            PersonalIdentityNumber = personalIdentityNumber;
        }

        public int? Id { get; init; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public string? PersonalIdentityNumber { get; init; }
    }
}