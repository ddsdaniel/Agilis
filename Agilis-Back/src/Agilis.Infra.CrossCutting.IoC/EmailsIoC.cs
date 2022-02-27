using Microsoft.Extensions.DependencyInjection;
using Agilis.Infra.Emails.Abstractions.Services;
using Agilis.Infra.Emails.Services;

namespace Agilis.Infra.CrossCutting.IoC
{
    public static class EmailsIoC
    {
        public static IServiceCollection AddEmailsIoC(this IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();
            return services;
        }
    }
}
