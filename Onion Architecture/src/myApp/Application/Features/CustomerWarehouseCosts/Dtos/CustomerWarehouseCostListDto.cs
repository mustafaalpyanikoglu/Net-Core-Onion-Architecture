using Core.Application.Dtos;

namespace Application.Features.CustomerWarehouseCosts.Dtos;

public class CustomerWarehouseCostListDto : IDto
{
    public int Id { get; set; }
    public int Capacity { get; set; }
    public double SetupCost { get; set; }
}
