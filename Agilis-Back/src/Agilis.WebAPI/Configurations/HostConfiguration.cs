using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Globalization;
using System.Text.Json.Serialization;

namespace Agilis.WebAPI.Configurations
{
    public static class HostConfiguration
    {
        public static IServiceCollection AddHostConfiguration(
            this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddResponseCompression();
            services.AddControllers(options =>
            {
                options.EnableEndpointRouting = false;
            })
            .AddNewtonsoftJson()
            .AddNewtonsoftConfig()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.IgnoreNullValues = true;
            });

            services.AddMemoryCache();

            services.AddApiVersioning(p =>
            {
                p.DefaultApiVersion = new ApiVersion(1, 0);
                p.ReportApiVersions = true;
                p.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddVersionedApiExplorer(p =>
            {
                p.GroupNameFormat = "'v'VVV";
                p.SubstituteApiVersionInUrl = true;
            });

            return services;
        }

        public static IApplicationBuilder UseAspNet(this IApplicationBuilder app)
        {
            app.UseResponseCompression();
            app.UseRouting();
            app.UseCorsConfig();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers())
               .UseApiVersioning()
               .UseMvcWithDefaultRoute();

            //TODO: app.UseHealthChecks("/health");

            return app;
        }

        public static IApplicationBuilder UseHostLocalizationBrasil(this IApplicationBuilder app)
        {
            var cultureInfo = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            return app;
        }

        public static IApplicationBuilder UseHostDeveloperExptionPage(
            this IApplicationBuilder app,
            IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            return app;
        }
    }
}
