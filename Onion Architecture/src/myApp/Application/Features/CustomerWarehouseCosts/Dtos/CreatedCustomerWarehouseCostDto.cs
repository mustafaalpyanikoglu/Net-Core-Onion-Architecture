using Core.Application.Dtos;

namespace Application.Features.CustomerWarehouseCosts.Dtos;

public class CreatedCustomerWarehouseCostDto:IDto
{
    public int Capacity { get; set; }
    public double SetupCost { get; set; }
}
