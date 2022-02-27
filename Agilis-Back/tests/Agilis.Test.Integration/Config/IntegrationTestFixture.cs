using AutoMapper;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq.AutoMock;
using Agilis.WebAPI;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Xunit;

namespace Agilis.Test.Integration.Config
{
    /// <summary>
    /// Mantém estado, não é recriado a cada teste, muito mais rápido
    /// </summary>
    /// <typeparam name="TStartup"></typeparam>
    public class IntegrationTestFixture : IDisposable,
        IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private IServiceProvider _services;

        public AutoMocker AutoMocker { get; private set; }
        public CustomWebApplicationFactory<Startup> Factory { get; private set; }
        public HttpClient HttpClient { get; private set; }
        public IMapper Mapper { get; private set; }


        public IntegrationTestFixture()
        {
            Factory = new CustomWebApplicationFactory<Startup>();
            InicializarHttpClient();
            InicializarServices();
            Mapper = _services.GetRequiredService<IMapper>();
            InicializarAutoMocker();
        }

        private void InicializarAutoMocker()
        {
            AutoMocker = new AutoMocker();
            AutoMocker.Use(Mapper);
        }

        private void InicializarServices()
        {
            var webHost = WebHost.CreateDefaultBuilder()
                   .UseStartup<Startup>()
                   .Build();

            var serviceScope = webHost.Services.CreateScope();
            _services = serviceScope.ServiceProvider;
        }

        private void InicializarHttpClient()
        {
            var options = new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri("https://localhost:5001")
            };
            HttpClient = Factory.CreateClient(options);
            HttpClient.DefaultRequestHeaders.Accept.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }       

        public void Dispose()
        {
            HttpClient.Dispose();
            Factory.Dispose();
        }
    }
}
