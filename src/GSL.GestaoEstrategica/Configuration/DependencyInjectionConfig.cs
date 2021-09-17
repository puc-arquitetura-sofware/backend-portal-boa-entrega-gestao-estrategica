using Amazon.CloudWatch;
using GSL.GestaoEstrategica.Data.Repositories;
using GSL.GestaoEstrategica.Dominio.Interfaces.Integration;
using GSL.GestaoEstrategica.Integration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GSL.GestaoEstrategica.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void AddGestaoEstrategicaServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRepositories();
            services.AddContext();
            services.AddAWSServices(configuration);
        }

        private static void AddRepositories(this IServiceCollection _)
        {
        }

        private static void AddContext(this IServiceCollection services)
        {
            services.AddScoped<GestaoEstrategicaDbContext>();
        }

        private static void AddAWSServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDefaultAWSOptions(configuration.GetAWSOptions());
            services.AddScoped<IAmazonCloudWatch, AmazonCloudWatchClient>();
            services.AddScoped<ICloudWatch, CloudWatch>();
        }

    }
}
