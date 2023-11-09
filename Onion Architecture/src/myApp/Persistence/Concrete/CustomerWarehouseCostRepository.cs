using Core.Persistence.Repositories;
using Domain.Concrete;
using Persistence.Contexts;
using Application.Services.Repositories;

namespace Persistence.Concrete;

public class CustomerWarehouseCostRepository : EfRepositoryBase<CustomerWarehouseCost, BaseDbContext>, ICustomerWarehouseCostRepository
{
    public CustomerWarehouseCostRepository(BaseDbContext context)
        : base(context) { }
}
