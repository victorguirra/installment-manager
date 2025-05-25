using InstallmentManager.Domain.Interfaces;
using InstallmentManager.Infraestructure.Data.Context;
using InstallmentManager.Infraestructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InstallmentManager.Infraestructure.Data
{
    public static class DependencyInjection
    {
        public static void ConfigureInfraestructureData(this IServiceCollection services, IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("DBConnection");
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IContractRepository, ContractRepository>();
            services.AddScoped<IInstallmentRepository, InstallmentRepository>();
            services.AddScoped<IInstallmentAnticipationRepository, InstallmentAnticipationRepository>();
        }
    }
}
