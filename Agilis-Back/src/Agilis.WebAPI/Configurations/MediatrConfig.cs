using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Agilis.WebAPI.Configurations
{
    public static class MediatrConfig
    {
        public static IServiceCollection AddMediatrConfig(this IServiceCollection services)
        {
            var assemblyDomainCore = AppDomain.CurrentDomain.Load("Agilis.Core.Domain");
            var assemblyAgilisApplication = AppDomain.CurrentDomain.Load("Agilis.Application");

            services.AddMediatR(config => { }, assemblyDomainCore, assemblyAgilisApplication);

            return services;
        }
    }
}
