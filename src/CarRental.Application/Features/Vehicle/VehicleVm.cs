using CarRental.Application.Features.Categories.Queries;

namespace CarRental.Application.Features.Vehicle;

public class VehicleVm
{
    public int VehicleId { get; set; }
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string PlateNumber { get; set; } = string.Empty;
    public CategoryVm Category { get; set; } = new();
    public List<CategoryVm> Categories { get; set; } = new();
}