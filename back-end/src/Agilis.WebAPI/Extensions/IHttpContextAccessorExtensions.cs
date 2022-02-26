using DDS.Domain.Core.Models.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace Agilis.WebAPI.Extensions
{
    /// <summary>
    /// Classe que provê métodos de extensão para IHttpContextAccessor
    /// </summary>
    public static class HttpContextAccessorExtensions
    {
        /// <summary>
        /// Obtém o e-mail do usuário logado
        /// </summary>
        /// <param name="httpContextAccessor">Variável injetada que contém os dados do usuário logado</param>
        /// <returns></returns>
        public static Email ObterEmail(this IHttpContextAccessor httpContextAccessor)
        {
            var emailLogado = new Email(httpContextAccessor.HttpContext.User.Identity.Name);
            return emailLogado;
        }
    }
}
