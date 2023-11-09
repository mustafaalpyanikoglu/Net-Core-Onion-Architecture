using Core.Persistence.Repositories;
using Domain.Concrete;

namespace Application.Services.Repositories;

public interface ICustomerWarehouseCostRepository : IAsyncRepository<CustomerWarehouseCost>, IRepository<CustomerWarehouseCost>
{
}
