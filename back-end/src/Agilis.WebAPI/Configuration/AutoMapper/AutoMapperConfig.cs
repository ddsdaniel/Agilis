using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Agilis.WebAPI.Configuration.AutoMapper.Profiles;

namespace Agilis.WebAPI.Configuration
{
    /// <summary>
    /// Define o mapeamento entre as entidades e as view models, bem como alguns value objects específicos
    /// </summary>
    public static class AutoMapperConfig
    {
        public static IServiceCollection AddAutoMapperConfig(this IServiceCollection services)
        {
            services.AddSingleton((provider) => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<PessoasProfile>();
                cfg.AddProfile<SegurancaProfile>();
                cfg.AddProfile<TrabalhoProfile>();
            }).CreateMapper());

            return services;
        }
    }
}