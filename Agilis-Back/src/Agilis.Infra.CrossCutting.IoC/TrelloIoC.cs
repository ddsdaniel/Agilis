using Agilis.Infra.Importacao.Trello.Abstractions.Factories;
using Agilis.Infra.Importacao.Trello.Abstractions.Services;
using Agilis.Infra.Importacao.Trello.Factories;
using Agilis.Infra.Importacao.Trello.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TrelloSharp.Abstractions.Services;
using TrelloSharpEasy.Services;

namespace Agilis.Infra.CrossCutting.IoC
{
    public static class TrelloIoC
    {
        public static IServiceCollection AddTrelloIoC(this IServiceCollection services)
        {
            //TODO: configurar
            string appKey = "8b51561a66106fabea5ab91fd31f86e2";
            string userToken = "111a737b23e7afe3eb94462753239f7377af921cdcf36480aeab17800faaa6a0";
            services.AddScoped(serviceProvider =>
            {
                var logger = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ILogger<Service>>();
                return new EasyService(appKey, userToken, logger);
            });
            services.AddScoped<IImportacaoTrelloService, ImportacaoTrelloService>();
            services.AddScoped<IFeatureFactory, FeatureFactory>();
            services.AddScoped<ITarefaFactory, TarefaFactory>();

            return services;
        }
    }
}
