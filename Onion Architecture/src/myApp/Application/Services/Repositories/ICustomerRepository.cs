using Core.Persistence.Paging;
using Core.Persistence.Repositories;
using Domain.Concrete;

namespace Application.Services.Repositories;

public interface ICustomerRepository : IAsyncRepository<Customer>, IRepository<Customer>
{
    Task<IPaginate<Customer>> GetListAsync(object index, object pageSize);
}
