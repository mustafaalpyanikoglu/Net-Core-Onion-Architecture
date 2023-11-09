using Core.Persistence.Repositories;
using Domain.Concrete;

namespace Application.Services.Repositories;

public interface IWarehouseRepository : IAsyncRepository<Warehouse>, IRepository<Warehouse>
{
}
