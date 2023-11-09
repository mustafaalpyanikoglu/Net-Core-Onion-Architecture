using Core.Persistence.Repositories;
using Domain.Concrete;
using Persistence.Contexts;
using Application.Services.Repositories;

namespace Persistence.Concrete;

public class CustomerRepository : EfRepositoryBase<Customer, BaseDbContext>, ICustomerRepository
{
    public CustomerRepository(BaseDbContext context)
        : base(context) { }
}
