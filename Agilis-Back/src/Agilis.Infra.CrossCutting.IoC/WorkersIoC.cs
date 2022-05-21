using Agilis.Application.Workers;
using Microsoft.Extensions.DependencyInjection;

namespace Agilis.Infra.CrossCutting.IoC
{
    public static class WorkersIoC
    {
        public static IServiceCollection AddWorkersIoC(this IServiceCollection services)
        {
            services.AddSingleton<LimpezaAnexosOrfaosWorker>();
            return services;
        }
    }
}
