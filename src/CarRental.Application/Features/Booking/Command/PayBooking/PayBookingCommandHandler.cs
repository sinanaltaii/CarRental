using CarRental.DataAccess;
using CarRental.Domains;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Application.Features.Booking.Command.PayBooking
{
    public class PayBookingCommandHandler : IRequestHandler<PayBookingCommand, decimal>, IAsyncDisposable
    {
        private readonly CarRentalContext _context;

        public PayBookingCommandHandler(CarRentalContext context)
        {
            _context = context;
        }

        public async Task<decimal> Handle(PayBookingCommand request, CancellationToken cancellationToken)
        {
            // This should be a transaction all or nothing
            var booking = await _context.Bookings
                        .Include(p => p.Vehicle)
                        .ThenInclude(p => p.Category)
                        .Include(p => p.Customer)
                        .FirstOrDefaultAsync(p => p.Id == request.ExtraBookingId, cancellationToken);

            var totalKm = request.MileageReading - booking.Vehicle.MileageReading;
            booking.ReturnDateTime = request.ReturnDateTime;
            var concreteCategoryType = GetConcreteCategoryType(booking.Category);
            booking.TotalCost = CalculateTotalCost(request, booking, totalKm);
            booking.Vehicle.IsAvailable = true;
            booking.Vehicle.MileageReading = request.MileageReading;
            _context.Entry(booking).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);

            return booking.TotalCost.Value;
        }

        private Category GetConcreteCategoryType(Category bookingCategory)
        {
            switch (bookingCategory.Name)
            {
                case CategoryNameAndDiscriminator.SmallCar:
                    return bookingCategory as SmallCar;
                case CategoryNameAndDiscriminator.StationWagon:
                    return bookingCategory as StationWagon;
                case CategoryNameAndDiscriminator.Truck:
                    return bookingCategory as Truck;
                default:
                    return bookingCategory as SmallCar;
            }
        }

        private decimal CalculateTotalCost(PayBookingCommand request, Domains.Booking booking, int totalKm)
        {
            switch (booking.Category.Name)
            {
                case CategoryNameAndDiscriminator.SmallCar:
                    return booking.Vehicle.Category.BasePrice * (int)Math.Ceiling((booking.ReturnDateTime - booking.PickUpDateTime).Value.TotalDays);
                case CategoryNameAndDiscriminator.StationWagon:
                    var stationWagon = booking.Category as StationWagon;
                    return booking.Vehicle.Category.BasePrice * (int)Math.Ceiling((booking.ReturnDateTime.Value - booking.PickUpDateTime).TotalDays) * stationWagon.AdditionalFee + stationWagon.BaseKmPrice * totalKm;
                case CategoryNameAndDiscriminator.Truck:
                    var truck = booking.Category as Truck;
                    return
                        booking.Vehicle.Category.BasePrice * (int)Math.Ceiling((booking.ReturnDateTime.Value - booking.PickUpDateTime).TotalDays) * truck.AdditionalFee + truck.BaseKmPrice * totalKm * 1.5m;

                default:
                    throw new ArgumentException("Category was not found");

            }
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
            GC.SuppressFinalize(this);
        }
    }
}
