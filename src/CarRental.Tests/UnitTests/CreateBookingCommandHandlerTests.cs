//using CarRental.Application.Contracts;
//using CarRental.Application.Features.Booking.Command.CreateBooking;
//using CarRental.Application.Features.Booking.Queries.GetBooking;
//using CarRental.Domains;
//using Moq;

//namespace CarRental.Tests.UnitTests
//{
//    public class CreateBookingCommandHandlerTests
//    {
//        private readonly Mock<IAsyncRepository<Domains.Booking>> _bookingRepository;
//        private readonly Mock<IAsyncRepository<Customer>> _customerRepository;
//        private readonly Mock<IAsyncRepository<Vehicle>> _vehicleRepository;
//        private readonly CreateBookingCommandHandler _handler;

//        public CreateBookingCommandHandlerTests()
//        {
//            _bookingRepository = new Mock<IAsyncRepository<Booking>>();
//            _customerRepository = new Mock<IAsyncRepository<Customer>>();
//            _vehicleRepository = new Mock<IAsyncRepository<Vehicle>>();

//            _handler = new CreateBookingCommandHandler(_bookingRepository.Object, _customerRepository.Object, _vehicleRepository.Object);
//        }

//        [Fact]
//        public async Task GivenWebIsUp_WhenCreatingBooking_ThenBookingIsCreated()
//        {
//            // Arrange
//            var command = new CreateBookingCommand()
//            {
//                Customer = new CustomerViewModel()
//                {
//                    FirstName = "Adam",
//                    LastName = "Smith",
//                    PersonalIdentityNumber = "123456789"
//                },
//                Id = 1,
//                MileageReading = 1000,
//                PickUpDateTime = DateTime.Now,
//                Id = 1
//            };
//            var booking = new Booking() { Id = 1 };
//            var customer = new Customer { Id = 1, LastName = "", FirstName = "", PersonalIdentityNumber = "123456789" };
//            var vehicle = new Vehicle { Id = 1, Model = "", Make = "", PlateNumber = "" }; // initializing with = string.Empty is better that having required
//            _customerRepository.Setup(x => x.AddAsync(It.IsAny<Customer>())).ReturnsAsync(customer);
//            _vehicleRepository.Setup(x => x.GetAsync(command.Id)).ReturnsAsync(vehicle);
//            _vehicleRepository.Setup(x => x.UpdateAsync(It.Is<Vehicle>(v => v.Id == 1 && v.IsAvailable == false))).Returns(Task.CompletedTask);
//            _bookingRepository.Setup(x => x.AddAsync(It.IsAny<Domains.Booking>())).ReturnsAsync(booking);
            
//            // Act
//            await _handler.Handle(command, CancellationToken.None);

//            // Assert
//            _vehicleRepository.Verify(x => x.UpdateAsync(It.Is<Domains.Vehicle>(v => v.Id == 1)), Times.Once);
//            _bookingRepository.Verify(x => x.AddAsync(It.Is<Domains.Booking>(b => b.Id == customer.Id && b.Id == command.Id && b.PickUpDateTime == command.PickUpDateTime && b.Id == command.Id)), Times.Once);
//        }
//    }
//}
