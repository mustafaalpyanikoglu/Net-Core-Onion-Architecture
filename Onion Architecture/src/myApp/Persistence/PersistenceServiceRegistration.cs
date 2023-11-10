using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Concrete;
using Application.Services.Repositories;
using Persistence.Repositories;

namespace Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BaseDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DBConnectionString"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddScoped<IOperationClaimRepository, OperationClaimRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserOperationClaimRepository, UserOperationClaimRepository>();
            services.AddScoped<IWarehouseRepository, WarehouseRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerWarehouseCostRepository, CustomerWarehouseCostRepository>();
            services.AddScoped<IEmailAuthenticatorRepository, EmailAuthenticatorRepository>();
            services.AddScoped<IOtpAuthenticatorRepository, OtpAuthenticatorRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            return services;
        }
    }
}
