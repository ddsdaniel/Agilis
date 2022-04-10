using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Agilis.Core.Domain.Abstractions.Models.ValueObjects;
using Agilis.Core.Domain.Models.ValueObjects.Configuracoes;

namespace Agilis.Infra.CrossCutting.IoC
{
    public static class ConfiguracoesIoC
    {
        public static IServiceCollection AddConfiguracoesIoC(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSettings>(appSetting =>
            {
                configuration.GetSection(nameof(AppSettings)).Bind(appSetting);
            });

            services.AddSingleton<IAppSettings>(serviceProvider => serviceProvider.GetRequiredService<IOptions<AppSettings>>().Value);

            return services;
        }
    }
}
