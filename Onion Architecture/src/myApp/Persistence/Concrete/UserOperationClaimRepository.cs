using Core.Persistence.Repositories;
using Domain.Concrete;
using Persistence.Contexts;
using Application.Services.Repositories;

namespace Persistence.Concrete;

public class UserOperationClaimRepository : EfRepositoryBase<UserOperationClaim, BaseDbContext>, IUserOperationClaimRepository
{
    public UserOperationClaimRepository(BaseDbContext context)
        : base(context) { }
}
