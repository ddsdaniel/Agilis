using Agilis.Application.Workers.Tarefas;
using Microsoft.Extensions.DependencyInjection;

namespace Agilis.Infra.CrossCutting.IoC
{
    public static class WorkersIoC
    {
        public static IServiceCollection AddWorkersIoC(this IServiceCollection services)
        {
            services.AddSingleton<ExcluirTagsNaoUsadasWorker>();
            return services;
        }
    }
}
