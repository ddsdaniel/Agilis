using DDS.Estatistica.Abstractions.Services;
using DDS.Estatistica.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Agilis.Infra.CrossCutting.IoC
{
    public static class EstatisticaIoC
    {
        public static IServiceCollection AddEstatisticaIoC(this IServiceCollection services)
        {
            services.AddTransient<IDesvioPadraoService, DesvioPadraoService>();
            services.AddTransient<IDesvioService, DesvioService>();
            services.AddTransient<IMedianaService, MedianaService>();
            services.AddTransient<IMediaService, MediaService>();
            services.AddTransient<IModaService, ModaService>();
            services.AddTransient<ITrimMeanService, TrimMeanService>();
            services.AddTransient<IVarianciaService, VarianciaService>();
            return services;
        }
    }
}
