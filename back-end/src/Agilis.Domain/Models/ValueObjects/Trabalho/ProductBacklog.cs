using Agilis.Domain.Models.ForeignKeys.Trabalho;
using DDS.Domain.Core.Abstractions.Model.ValueObjects;
using DDS.Domain.Core.Extensions;
using Flunt.Validations;
using System.Collections.Generic;
using System.Linq;

namespace Agilis.Domain.Models.ValueObjects.Trabalho
{
    public class ProductBacklog : ValueObject<ProductBacklog>
    {
        public IEnumerable<Fase> Fases { get; private set; }

        public ProductBacklog()
            : this(new List<Fase>())
        {

        }

        public ProductBacklog(IEnumerable<Fase> fases)
        {
            AddNotifications(new Contract()
                .IsValidArray(fases, nameof(Fases))
                );

            Fases = fases;
        }

        internal Fase AdicionarFase(string nome)
        {
            if (Fases.Any(f => f.Nome == nome))
            {
                AddNotification(nameof(nome), "Já existe uma fase com este nome");
                return null;
            }

            var posicao = Fases.Any() ? Fases.Max(f => f.Posicao) + 1 : 1;

            var fase = new Fase(posicao, nome, new List<TarefaFK>());
            if (fase.Invalid)
            {
                AddNotifications(fase);
                return null;
            }

            var novaLista = Fases.ToList();
            novaLista.Add(fase);

            Fases = novaLista;

            return fase;
        }
    }
}
