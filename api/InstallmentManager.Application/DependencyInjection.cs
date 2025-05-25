using InstallmentManager.Application.Services;
using InstallmentManager.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace InstallmentManager.Application
{
    public static class DependencyInjection
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            // AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Services;
            services.AddScoped<IUserContextService, UserContextService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IContractService, ContractService>();
            services.AddScoped<IInstallmentService, InstallmentService>();
            services.AddScoped<IInstallmentAnticipationService, InstallmentAnticipationService>();
        }
    }
}
