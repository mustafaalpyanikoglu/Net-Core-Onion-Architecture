using Core.Persistence.Repositories;
using Domain.Concrete;

namespace Application.Services.Repositories;

public interface IEmailAuthenticatorRepository : IAsyncRepository<EmailAuthenticator>, IRepository<EmailAuthenticator> { }
