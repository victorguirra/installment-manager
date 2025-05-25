using InstallmentManager.Domain.Interfaces;
using InstallmentManager.Infraestructure.Security.Services;
using Microsoft.Extensions.DependencyInjection;

namespace InstallmentManager.Infraestructure.Security
{
    public static class DependencyInjection
    {
        public static void ConfigureInfraestructureSecurity(this IServiceCollection services)
        {
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<ITokenService, TokenService>();
        }
    }
}
