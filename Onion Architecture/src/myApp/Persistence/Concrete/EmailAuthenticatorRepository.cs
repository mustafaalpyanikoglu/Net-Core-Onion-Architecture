using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Concrete;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class EmailAuthenticatorRepository : EfRepositoryBase<EmailAuthenticator, BaseDbContext>, IEmailAuthenticatorRepository
{
    public EmailAuthenticatorRepository(BaseDbContext context)
        : base(context) { }
}

