using CarRental.Application.Features.Booking.Queries.GetBooking;
using MediatR;

namespace CarRental.Application.Features.Booking.Command.CreateBooking
{
    public class CreateBookingCommand : IRequest
    {
        public DateTime PickUpDateTime { get; set; }
        public DateTime ReturnDateTime { get; set; }
        public int VehicleId { get; set; }
        public int MileageReading { get; set; }
        public int CategoryId { get; set; }
        public int CustomerId { get; set; }
        public CustomerViewModel Customer { get; set; }
    }
}
