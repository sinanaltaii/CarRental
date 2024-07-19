using MediatR;

namespace CarRental.Application.Features.Booking.Command.PayBooking
{
    public class PayBookingCommand : IRequest<decimal>
    {
        public int BookId { get; set; }
        public int ExtraBookingId { get; set; }
        public int MileageReading { get; set; }
        public DateTime ReturnDateTime { get; set; }
    }
}
