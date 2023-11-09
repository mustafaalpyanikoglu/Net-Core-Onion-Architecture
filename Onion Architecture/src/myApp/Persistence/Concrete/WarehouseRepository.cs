using Core.Persistence.Repositories;
using Domain.Concrete;
using Persistence.Contexts;
using Application.Services.Repositories;

namespace Persistence.Concrete;

public class WarehouseRepository : EfRepositoryBase<Warehouse, BaseDbContext>, IWarehouseRepository
{
    public WarehouseRepository(BaseDbContext context)
        : base(context) { }
}
