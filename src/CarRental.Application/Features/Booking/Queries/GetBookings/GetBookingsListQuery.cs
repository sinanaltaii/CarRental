using MediatR;

namespace CarRental.Application.Features.Booking.Queries.GetBookings
{
    public class GetBookingsListQuery : IRequest<List<BookingViewModel>>
    {
    }
}
