namespace CarRental.Domains;

public class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PersonalIdentityNumber { get; set; } = string.Empty;
    public ContactInfo? ContactInfo { get; set; }

    public List<Booking> Bookings { get; set; } = [];
}

public class ContactInfo
{
    public int Id { get; set; }
    public string StreetAddress { get; set; } = string.Empty;
    public string? StreetAddressLine2 { get; set; }
    public string PostalCode { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string? AlternatePhoneNumber { get; set; } 
    public string? EmailAddress { get; set;} = string.Empty;

    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
}