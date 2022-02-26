﻿using DDS.Domain.Core.Abstractions.Models.ValueObjects;
using Flunt.Validations;
using System.Collections.Generic;
using System.Linq;

namespace Agilis.Domain.Models.ValueObjects.Especificacao
{
    public class LinguagemUbiqua : ValueObject<LinguagemUbiqua>
    {
        public IEnumerable<JargaoDoNegocio> Jargoes { get; private set; }

        public LinguagemUbiqua(IEnumerable<JargaoDoNegocio> jargoes)
        {
            AddNotifications(new Contract()
                .IsNotNull(jargoes, nameof(Jargoes), "Jargões não pode ser nulo")
                .IfNotNull(jargoes, c => c.Join(jargoes.ToArray()))
                );

            Jargoes = jargoes;
        }

        public void Adicionar(string jargao, string significado)
        {
            var jargaoDoNegocio = new JargaoDoNegocio(jargao, significado);
            AddNotifications(jargaoDoNegocio);

            if (Valid)
            {
                if (ContainsKey(jargao))
                {
                    AddNotification(nameof(Jargoes), "Já duplicado");
                }
                else
                {
                    var novaLista = Jargoes.ToList();
                    novaLista.Add(jargaoDoNegocio);
                    Jargoes = novaLista;
                }
            }
        }

        public bool ContainsKey(string jargao) => Jargoes.Any(j => j.Jargao.ToLower() == jargao.ToLower());

        public void Remover(string jargao)
        {
            if (!ContainsKey(jargao))
            {
                AddNotification(nameof(jargao), "Jargão não encontrado");
            }
            else
            {
                var novaLista = Jargoes.ToList();
                novaLista.RemoveAll(j => j.Jargao.ToLower() == jargao.ToLower());
                Jargoes = novaLista;
            }
        }

    }
}
