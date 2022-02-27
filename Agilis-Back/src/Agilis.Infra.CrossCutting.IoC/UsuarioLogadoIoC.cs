using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using Agilis.Core.Domain.Models.ValueObjects;
using Agilis.Infra.Seguranca.Abstractions.Models.Entities;
using Agilis.Infra.Seguranca.Models.Entities;
using System;
using System.Linq;

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

            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWorkCatalogo>();
            var usuarioRepository = unitOfWork.ObterRepository<Usuario>();

            var usuario = usuarioRepository.Consultar().FirstOrDefault(u => u.Email.Endereco == email.Endereco);

            scope.Dispose();

            return usuario;
        }
    }
}
