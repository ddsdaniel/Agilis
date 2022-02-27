using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;

namespace Agilis.WebAPI.Configurations
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services,
                                                    IWebHostEnvironment enviroment)
        {
            //if (enviroment.IsDevelopment() || enviroment.IsStaging())
            //{
            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Agilis API",
                        Version = "v1",
                        Description = "",
                        Contact = new OpenApiContact
                        {
                            Name = "Agilis",
                            Url = new Uri("http://www.lojinha.app")
                        }
                    });

                    //habilitar a autenticacao pela SwaggerUI
                    setupAction.AddSecurityDefinition("Authorization", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Description = "Autenticação do usuário"
                });

                setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Authorization" },
                            },
                            Array.Empty<string>()
                        }
                    });

                setupAction.OrderActionsBy(apiDesc => $"{apiDesc.RelativePath}_{apiDesc.HttpMethod}");

                setupAction.CustomSchemaIds(x => x.FullName);

            });
            //}
            return services;
        }

        public static IApplicationBuilder UseSwagger(this IApplicationBuilder app,
                                                     IWebHostEnvironment env,
                                                     IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
                }

                c.DocExpansion(DocExpansion.List);
            });
            return app;
        }
    }
}
