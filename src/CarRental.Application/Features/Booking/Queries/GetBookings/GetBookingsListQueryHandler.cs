using CarRental.Application.Features.Booking.Queries.GetBooking;
using CarRental.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Application.Features.Booking.Queries.GetBookings
{
    public class GetBookingsListQueryHandler : IRequestHandler<GetBookingsListQuery, List<BookingViewModel>>, IAsyncDisposable
    {
        private readonly CarRentalContext _context;

        public GetBookingsListQueryHandler(CarRentalContext context)
        {
            _context = context;
        }

        public async Task<List<BookingViewModel>> Handle(GetBookingsListQuery request, CancellationToken cancellationToken)
        {
            var bookings = await _context.Bookings
                .Include(v => v.Vehicle)
                .ThenInclude(p => p.Category)
                .Include(p => p.Customer)
                .Where(b => b.ReturnDateTime == null)
                .ToListAsync(cancellationToken: cancellationToken);
            return bookings.Select(booking => new BookingViewModel()
            {
                BookingId = booking.Id,
                Customer = new CustomerViewModel
                {
                    CustomerId = booking.CustomerId,
                    FirstName = booking.Customer.FirstName,
                    LastName = booking.Customer.LastName,
                    PersonalIdentityNumber = booking.Customer.PersonalIdentityNumber
                },
                Vehicle = new BookingVehicle
                {
                    VehicleId = booking.Vehicle.Id,
                    ModelAndMake = $"{booking.Vehicle.Make} {booking.Vehicle.Model}"
                },
                Category = new BookingCategory
                {
                    CategoryId = booking.Vehicle.CategoryId.Value,
                    BasePrice = booking.Vehicle.Category.BasePrice
                },
            }).ToList();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
            GC.SuppressFinalize(this);
        }
    }
}
