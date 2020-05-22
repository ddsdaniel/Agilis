using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Linq;

namespace Agilis.WebAPI.Tests.Integracao.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void ReplaceScoped<TService>(this IServiceCollection services, TService servico)
            where TService : class
        {
            // Remove the service registration.
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(TService));
            if (descriptor != null)
                services.Remove(descriptor);

            // Add service registration for testing.
            services.TryAddScoped(x => servico);
        }
    }
}
