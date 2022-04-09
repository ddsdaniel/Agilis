using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.Contracts;

namespace Agilis.Test.Integration.Config
{
    public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            Contract.Assume(builder != null);

            if (builder == null)
                return;

            builder.UseEnvironment(Environments.Staging);
        }
    }
}
