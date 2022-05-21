using Agilis.Infra.Importacao.Trello.Abstractions.Services;
using Agilis.Infra.Importacao.Trello.Services;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddScoped(serviceProvider => new EasyService(appKey, userToken));
            services.AddScoped<IImportacaoTrelloService, ImportacaoTrelloService>();

            return services;
        }
    }
}
