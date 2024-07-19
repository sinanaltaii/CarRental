//using CarRental.Application.Contracts;
//using CarRental.Application.Features.Booking.Command.PayBooking;
////using CarRental.DataAccess.Repositories;
//using CarRental.Domains;
//using Moq;

//namespace CarRental.Tests.UnitTests
//{
//    public class PayBookingCommandHandlerTests
//    {
//        private readonly Mock<IBookingRepository> _repository;
//        private readonly PayBookingCommandHandler _handler;

//        public PayBookingCommandHandlerTests()
//        {
//            _repository = new Mock<IBookingRepository>();
//            //_handler = new PayBookingCommandHandler(_repository.Object);
//        }

//        [Fact]
//        public async Task GivenWebIsUp_WhenPayingABooking_ThenBookingIsUpdatedAndPaid()
//        {
//            var command = new PayBookingCommand
//            {
//                ExtraBookingId = 1,
//                MileageReading = 35,
//                ReturnDateTime = DateTime.Now
//            };

//            var booking = new Booking()
//            {
//                Id = 1,
//                PickUpDateTime = DateTime.Now.AddDays(-3),
//                Vehicle = new Vehicle()
//                {
//                    Odometer = 25,
//                    IsAvailable = false,
//                    PlateNumber = "OSA-123",
//                    Make = "Volvo",
//                    Model = "XC90",
//                    Category = new Category()
//                    {
//                        Name = "Kombi",
//                        BasePrice = 10,
//                        BaseKmPrice = 1.3m
//                    }
//                },
//                TotalCost = 0
//            };
//            var basePrice = booking.Vehicle.Category.BasePrice;
//            var rentalPeriodInDays = (int)Math.Ceiling((command.ReturnDateTime - booking.PickUpDateTime).TotalDays);
//            var baseKmPrice = booking.Vehicle.Category.BaseKmPrice.Value;
//            var totalKm = command.MileageReading - booking.Vehicle.Odometer;
//            var totalPriceActual = basePrice * rentalPeriodInDays* 1.3m + baseKmPrice * totalKm;
//            _repository.Setup(x => x.GetBookingInfoAsync(command.ExtraBookingId)).ReturnsAsync(booking);
//            _repository.Setup(x => x.UpdateAsync(It.IsAny<Booking>())).Returns(Task.CompletedTask);

//            var result = await _handler.Handle(command, CancellationToken.None);
//            //return booking.Vehicle.Category.BasePrice * (int)Math.Ceiling((booking.ReturnDateTime.Value - booking.PickUpDateTime).TotalDays) * 1.3m + booking.Vehicle.Category.BaseKmPrice.Value * totalKm;
//            _repository.Verify(x => x.GetBookingInfoAsync(command.ExtraBookingId), Times.Once);
//            _repository.Verify(x => x.UpdateAsync(It.Is<Booking>(b =>
//                b.Vehicle.IsAvailable == true &&
//                b.Vehicle.Odometer == command.MileageReading)));

//            Assert.Equal(totalPriceActual, result);
//        }
//    }
//}
