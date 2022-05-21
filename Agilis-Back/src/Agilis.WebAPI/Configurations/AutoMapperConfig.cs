using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Agilis.WebAPI.Configurations
{
    public static class AutoMapperConfig
    {
        public static IServiceCollection AddAutoMapperConfig(this IServiceCollection services)
        {
            //var configuration = new MapperConfiguration(cfg =>
            //    cfg.AddMaps(new[] {
            //        "Agilis.Application"
            //    })
            //);

            services.AddSingleton((provider) => new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(new[] { "Agilis.Application" });
                cfg.AddMaps(new[] { "Agilis.Infra.Importacao.Trello" });
                //cfg.AddProfile<DomainToViewModelProfile>();
                //cfg.AddProfile<OutrosProfile>();
                //cfg.AddProfile<DomainToGoogleFcmProfile>();
            }).CreateMapper());

            return services;
        }
    }
}
