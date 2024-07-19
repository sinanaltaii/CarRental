using System.ComponentModel;

namespace CarRental.Application.Features.Booking.Queries.GetBooking;

public class CustomerViewModel
{
    public int CustomerId { get; set; }

    [DisplayName("First name")]
    public required string FirstName { get; set; }

    [DisplayName("Last name")]
    public required string LastName { get; set; }

    [DisplayName("Personal identity number")]
    public required string PersonalIdentityNumber { get; set; }
}