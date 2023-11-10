using Core.Application.Dtos;

namespace Application.Features.Warehouses.Dtos;

public class CreatedWarehouseDto:IDto
{
    public int Capacity { get; set; }
    public double SetupCost { get; set; }
}
