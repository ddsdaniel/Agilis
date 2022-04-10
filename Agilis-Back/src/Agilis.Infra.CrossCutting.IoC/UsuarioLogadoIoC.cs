using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using Agilis.Core.Domain.Models.ValueObjects;
using System;
using System.Linq;
using Agilis.Core.Domain.Models.Entities.Seguranca;
using Agilis.Core.Domain.Abstractions.Models.Entities;

namespace Agilis.Infra.CrossCutting.IoC
{
    public static class UsuarioLogadoIoC
    {
        public static IServiceCollection AddUsuarioLogadoIoC(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(serviceProvider => ObterUsuarioLogado(serviceProvider));
            return services;
        }

        private static IUsuario ObterUsuarioLogado(IServiceProvider serviceProvider)
        {
            var httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
            var email = new Email(httpContextAccessor.HttpContext.User.Identity.Name);

            var scope = serviceProvider.CreateScope();

            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var usuarioRepository = unitOfWork.ObterRepository<Usuario>();

            var usuario = usuarioRepository.Consultar().FirstOrDefault(u => u.Email.Endereco == email.Endereco);

            scope.Dispose();

            return usuario;
        }
    }
}
