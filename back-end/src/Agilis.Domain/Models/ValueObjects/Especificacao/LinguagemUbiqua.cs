using DDS.Domain.Core.Abstractions.Model.ValueObjects;
using Flunt.Validations;
using System.Collections.Generic;
using System.Linq;

namespace Agilis.Domain.Models.ValueObjects.Especificacao
{
    public class LinguagemUbiqua : ValueObject<LinguagemUbiqua>
    {
        public ICollection<JargaoDoNegocio> Jargoes { get; private set; }

        public LinguagemUbiqua(ICollection<JargaoDoNegocio> jargoes)
        {
            AddNotifications(new Contract()
                .IsNotNull(jargoes, nameof(Jargoes), "Jargões não pode ser nulo")
                .IfNotNull(jargoes, c => c.Join(jargoes.ToArray()))
                );

            Jargoes = jargoes;
        }

    }
}
