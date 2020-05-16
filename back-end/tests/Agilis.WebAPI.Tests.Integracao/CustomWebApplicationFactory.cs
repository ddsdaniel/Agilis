using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Agilis.WebAPI.Tests.Integracao.Extensions;
using MongoDB.Driver;
using Microsoft.Extensions.DependencyInjection;
using Agilis.Domain.Abstractions.Repositories;
using Microsoft.Extensions.Logging;
using Agilis.WebAPI.Tests.Integracao.Helpers;

namespace Agilis.WebAPI.Tests.Integracao
{
    public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup : class
    {

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var mongoDatabase = new MongoClient(
                    new MongoClientSettings
                    {
                        ReplicaSetName = "rs1"
                    }).GetDatabase("agilis-testes");
                services.ReplaceScoped(mongoDatabase);

                // Build the service provider.
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database context
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var unitOfWork = scopedServices.GetRequiredService<IUnitOfWork>();
                    var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    InitializeDbForTests.Inicializar(unitOfWork);
                }
            });
        }
    }
}