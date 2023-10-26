using Core.Persistence.Repositories;
using Domain.Concrete;

namespace Application.Services.Repositories;
public interface IUserOperationClaimRepository : IAsyncRepository<UserOperationClaim>, IRepository<UserOperationClaim> { }
