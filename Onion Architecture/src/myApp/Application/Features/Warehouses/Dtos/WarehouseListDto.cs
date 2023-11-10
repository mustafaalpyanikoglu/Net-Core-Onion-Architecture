using Core.Application.Dtos;

namespace Application.Features.Warehouses.Dtos;

public class WarehouseListDto : IDto
{
    public int Id { get; set; }
    public int Capacity { get; set; }
    public double SetupCost { get; set; }
}
