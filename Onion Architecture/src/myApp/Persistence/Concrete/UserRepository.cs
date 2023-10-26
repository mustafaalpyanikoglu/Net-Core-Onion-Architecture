using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Concrete;
using Persistence.Contexts;

namespace Persistence.Concrete;

public class UserRepository : EfRepositoryBase<User, BaseDbContext>, IUserRepository
{
    public UserRepository(BaseDbContext context)
        : base(context) { }
}
