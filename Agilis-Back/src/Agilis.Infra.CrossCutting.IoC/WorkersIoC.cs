using Microsoft.Extensions.DependencyInjection;

namespace Agilis.Infra.CrossCutting.IoC
{
    public static class WorkersIoC
    {
        public static IServiceCollection AddWorkersIoC(this IServiceCollection services)
        {
            return services;
        }
    }
}
