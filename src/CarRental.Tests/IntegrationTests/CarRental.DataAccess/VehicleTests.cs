using CarRental.Domains;
using CarRental.Tests.IntegrationTests.Setup;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Tests.IntegrationTests.CarRental.DataAccess
{
    [Collection("CarRentalContextCollection")]
    public class VehicleTests
    {
        private readonly CarRentalContextFixture _fixture;

        public VehicleTests(CarRentalContextFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GivenDbIsUp_WhenRetrievingVehicle_ThenVehicleReturned()
        {
            var vehicle = await _fixture.Context.Vehicles.FindAsync(1);

            Assert.NotNull(vehicle);
            Assert.Equal(1, vehicle.Id);
        }

        [Fact]
        public async Task GivenDbIsUp_WhenAddingVehicle_ThenVehicleAdded()
        {
            var vehicle = new Vehicle { Model = "X90", Make = "Volvo", PlateNumber = "TUA-555", CategoryId = 1, IsAvailable = true, MileageReading = 9 };
            await _fixture.Context.Vehicles.AddAsync(vehicle);
            await _fixture.Context.SaveChangesAsync();

            Assert.NotEqual(0, vehicle.Id);
        }

        [Fact]
        public async Task GivenDbIsUp_WhenUpdatingVehicle_ThenVehicleUpdated()
        {
            var vehicle = await _fixture.Context.Vehicles.FindAsync(1);
            var newPlateNumber = "OSA-390";
            vehicle.PlateNumber = newPlateNumber;
            vehicle.MileageReading = 11;
            vehicle.IsAvailable = false;
            _fixture.Context.Entry(vehicle).State = EntityState.Modified;
            await _fixture.Context.SaveChangesAsync();

            var updatedVehicle = await _fixture.Context.Vehicles.FindAsync(vehicle.Id);
            Assert.Equal(newPlateNumber, updatedVehicle.PlateNumber);
        }

        [Fact]
        public async Task GivenDbIsUp_WhenDeletingVehicle_ThenVehicleDeleted()
        {
            var vehicle = new Vehicle { Model = "Mazda", Make = "Old", PlateNumber = "Bye-111", CategoryId = 1 };
            await _fixture.Context.AddAsync(vehicle);
            await _fixture.Context.SaveChangesAsync();
            var vehicleId = vehicle.Id;

            _fixture.Context.Vehicles.Remove(vehicle);
            await _fixture.Context.SaveChangesAsync();

            var removedVehicle = await _fixture.Context.Vehicles.FindAsync(vehicleId);

            Assert.Null(removedVehicle);
        }

    }
}
