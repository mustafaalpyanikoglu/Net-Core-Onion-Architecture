using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Concrete;
using Persistence.Contexts;

namespace Persistence.Concrete;

public class OperationClaimRepository : EfRepositoryBase<OperationClaim, BaseDbContext>, IOperationClaimRepository
{
    public OperationClaimRepository(BaseDbContext context)
        : base(context) { }
}
