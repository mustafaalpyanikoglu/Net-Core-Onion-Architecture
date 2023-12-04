using Domain.Concrete;

namespace Application.Services.CustomerService;

public interface ICustomerWarehouseCostService
{
    Task<List<CustomerWarehouseCost>> GetWarehouseCustomerCosts();
    Task<List<CustomerWarehouseCost>> GetListCustomerWarehouseCosts();
}
