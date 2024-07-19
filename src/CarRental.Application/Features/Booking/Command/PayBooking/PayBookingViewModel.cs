namespace CarRental.Application.Features.Booking.Command.PayBooking
{
    public class PayBookingViewModel
    {
        public int BookId { get; set; }
        public int MileageReading { get; set; }
        public DateTime ReturnDateTime { get; set; }
    }
}
