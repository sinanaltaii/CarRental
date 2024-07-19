using MediatR;

namespace CarRental.Application.Features.Vehicle.Queries.GetVehicles
{
    public class GetVehiclesListQuery : IRequest<List<VehicleVm>>
    {
    }
}
