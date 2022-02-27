using Microsoft.Extensions.DependencyInjection;
using Agilis.Core.Domain.Abstractions.Services;
using Agilis.Core.Domain.Services;

namespace Agilis.Infra.CrossCutting.IoC
{
    public static class CriptografiaIoC
    {
        public static IServiceCollection AddCriptografiaIoC(this IServiceCollection services)
        {
            services.AddScoped<ICriptografiaSimetrica, AdvancedEncryptionStandardService>();
            return services;
        }
    }
}
