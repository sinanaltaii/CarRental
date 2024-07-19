using System.ComponentModel;

namespace CarRental.Application.Features.Booking
{
    public class BookingVehicle
    {
        public int VehicleId { get; set; }
        [DisplayName("Vehicle")]
        public string ModelAndMake { get; set; } = string.Empty;
    }
}
