using Core.Persistence.Repositories;
using Domain.Concrete;

namespace Application.Services.Repositories;

public interface IRefreshTokenRepository : IAsyncRepository<RefreshToken>, IRepository<RefreshToken> { }
