using LightsOut.Engine;
using LightsOut.Engine.Interfaces;
using LightsOut.Random;
using LightsOut.Random.Interfaces;
using LightsOut.Repository.Interfaces;
using LightsOut.Repository.MySql;
using LightsOut.Repository.MySql.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LightsOut.Dependencies
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLightsOutEngine(this IServiceCollection services, Action<LightsOutMySqlConfiguration> options)
        {
            services.AddScoped<ILightsOutEngine, LightsOutEngine>()
                .AddSingleton<IRandomProvider, RandomProvider>()
                .AddSingleton<ILightsOutRepository, LightsOutRepository>();

            services.AddSingleton(x =>
            {
                var configuration = new LightsOutMySqlConfiguration();
                options(configuration);
                return configuration;
            });

            return services;
        }
    }
}