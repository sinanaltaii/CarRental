using System.ComponentModel;
using CarRental.Application.Features.Booking.Queries.GetBooking;
using CarRental.Application.Features.Categories.Queries;
using CarRental.Application.Features.Vehicle;

namespace CarRental.Application.Features.Booking.Queries;

public class BookingViewModel
{
    [DisplayName("Booking Id")]
    public int BookingId { get; set; }
    public int ExtraBookingId { get; set; }
    public DateTime PickUpDateTime { get; set; }
    public DateTime ReturnDateTime { get; set; }
    public decimal? TotalCost { get; set; }
    public int MileageReading { get; set; }

    public CustomerViewModel? Customer { get; set; }
    public BookingVehicle? Vehicle { get; set; }
    public BookingCategory? Category { get; set; }
    public List<CategoryVm> Categories { get; set; }
    public List<VehicleVm> Vehicles { get; set; }
}