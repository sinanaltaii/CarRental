using MediatR;

namespace CarRental.Application.Features.Booking.Queries.GetBooking
{
    public class GetBookingQuery : IRequest<BookingViewModel>
    {
        public int BookingId { get; set; }
    }
}
