using InstallmentManager.Infraestructure.Data;
using InstallmentManager.Infraestructure.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InstallmentManager.Infraestructure.IoC
{
    public static class DependencyInjection
    {
        public static void AddInfraestructureServices(this IServiceCollection services, IConfigurationManager configuration)
        {
            services.ConfigureInfraestructureData(configuration);
            services.ConfigureInfraestructureSecurity();
        }
    }
}
