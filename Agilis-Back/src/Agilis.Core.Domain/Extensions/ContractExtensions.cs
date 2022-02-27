using Flunt.Notifications;
using Flunt.Validations;
using System.Collections.Generic;
using System.Linq;

namespace Agilis.Core.Domain.Extensions
{
    public static class ContractExtensions
    {
        public static Contract IsValidArray<T>(this Contract contrato, IEnumerable<T> colecao, string propriedade)
            where T : Notifiable
        {
            contrato.IsNotNull(colecao, propriedade, $"Lista de {propriedade} não pode ser nula")
                    .IfNotNull(colecao, c => c.Join(colecao.ToArray()));

            return contrato;
        }

        /// <summary>
        /// Valida se é diferente de nulo, se for, acrescenta as notificações deste objeto
        /// </summary>
        /// <param name="contrato"></param>
        /// <param name="notifiable"></param>
        /// <param name="propriedade"></param>
        /// <returns></returns>
        public static Contract IsValid(this Contract contrato, Notifiable notifiable, string propriedade)
        {
            contrato.IsNotNull(notifiable, propriedade, $"{propriedade} não pode ser nulo")
                    .IfNotNull(notifiable, c => c.Join(notifiable));

            return contrato;
        }
    }
}
