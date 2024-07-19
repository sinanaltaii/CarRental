using CarRental.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Application.Features.Booking.Queries.GetBooking
{
    public class GetBookingQueryHandler : IRequestHandler<GetBookingQuery, BookingViewModel>, IAsyncDisposable
    {
        private readonly CarRentalContext _context;

        public GetBookingQueryHandler(CarRentalContext context)
        {
            _context = context;
            //_repository = repository;
        }

        public async Task<BookingViewModel> Handle(GetBookingQuery request, CancellationToken cancellationToken)
        {
            //var booking = await _repository.GetBookingInfoAsync(request.Id);
            var booking = await _context.Bookings
                .AsNoTracking()
                .Include(p => p.Vehicle)
                .ThenInclude(p => p.Category)
                .Include(p => p.Customer)
                .FirstOrDefaultAsync(p => p.Id == request.BookingId, cancellationToken);

            return new BookingViewModel()
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
            };
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
            GC.SuppressFinalize(this);
        }
    }
}
