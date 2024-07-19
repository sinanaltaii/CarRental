using CarRental.DataAccess;
using CarRental.Domains;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Application.Features.Booking.Command.CreateBooking
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand>, IAsyncDisposable
    {
        private readonly CarRentalContext _context;

        public CreateBookingCommandHandler(CarRentalContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            // This should be handled as transaction, so we don't end up with half made booking
            var customer = new Domains.Customer()
            {
                FirstName = request.Customer.FirstName,
                LastName = request.Customer.LastName,
                PersonalIdentityNumber = request.Customer.PersonalIdentityNumber
            };
            await _context.Customers.AddAsync(customer, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);


            var vehicle = await _context.Vehicles.FindAsync(request.VehicleId);
            vehicle.IsAvailable = false;
            vehicle.MileageReading = request.MileageReading;
            _context.Entry(vehicle).State = EntityState.Modified;

            await _context.Bookings.AddAsync(new Domains.Booking()
            {
                CustomerId = customer.Id,
                PickUpDateTime = request.PickUpDateTime,
                VehicleId = request.VehicleId,
                CategoryId = request.CategoryId,
            }, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
            GC.SuppressFinalize(this);
        }
    }
}
