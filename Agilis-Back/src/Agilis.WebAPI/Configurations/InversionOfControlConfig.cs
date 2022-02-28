using Agilis.Infra.CrossCutting.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Agilis.Core.Domain.Abstractions.Services;
using Agilis.Infra.Notifications.FirebaseCloudMessaging.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Agilis.WebAPI.Configurations
{
    public static class InversionOfControlConfig
    {
        public static IServiceCollection AddInversionOfControlConfig(
            this IServiceCollection services,
            IConfiguration configuration,
            IWebHostEnvironment currentEnvironment)
        {
            services.AddConfiguracoesIoC(configuration);
            services.AddUsuarioLogadoIoC();
            services.AddEmailsIoC();
            services.AddCriptografiaIoC();
            if (!currentEnvironment.IsStaging())
            {
                services.AddDatabaseIoC(configuration);
            }
            services.AddAppServicesIoC();
            services.AddWorkersIoC();
            services.AddSingleton<INotificationService, GoogleFcmNotificationService>();
            services.AddEstatisticaIoC();

            return services;
        }
    }
}
