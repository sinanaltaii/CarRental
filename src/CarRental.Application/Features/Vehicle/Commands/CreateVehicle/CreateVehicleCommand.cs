using MediatR;

namespace CarRental.Application.Features.Vehicle.Commands.CreateVehicle
{
    public class CreateVehicleCommand : IRequest
    {
        public int VehicleId { get; set; }
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string PlateNumber { get; set; } = string.Empty;
        public int CategoryId { get; set; }
    }
}
