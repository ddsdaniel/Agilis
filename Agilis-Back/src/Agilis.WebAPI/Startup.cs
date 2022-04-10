using Agilis.WebAPI.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Agilis.WebAPI.HostedService;
using DDS.Logs.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Agilis.WebAPI
{
    public class Startup
    {
        private readonly IWebHostEnvironment _currentEnvironment;
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _currentEnvironment = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddResponseCompression(options => options.EnableForHttps = true);
            services.AddCors();
            services.AddInversionOfControlConfig(_configuration, _currentEnvironment);
            services.AddHostConfiguration();
            services.AddSwagger(_currentEnvironment);
            services.AddAutoMapperConfig();
            services.AddMediatrConfig();
            services.AddJwtConfig();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            if (!_currentEnvironment.IsStaging())
            {
                services.AddHostedService<SchedulerBackgroundService>();
                services.AddHostedService<MigrationHostedService>();
            }
        }

        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            IApiVersionDescriptionProvider provider)
        {
            app.UseResponseCompression();
            app.UseMiddleware<RequestLoggingMiddleware>();
            app.UseHostLocalizationBrasil();
            app.UseHostDeveloperExptionPage(env);
            app.UseSwagger(env, provider);
            app.UseAspNet();
            app.UseHttpsRedirection();
        }
    }
}
