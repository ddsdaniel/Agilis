using Bogus;
using Agilis.Core.Domain.Models.Entities;
using Agilis.Core.Domain.Models.ValueObjects;
using Agilis.Test.Mock.Domain.Models.Entities;
using System.Collections.Generic;

namespace Agilis.Test.Mock.Domain.Models.ValueObjects
{
    public static class NotificacaoMocks
    {
        public static Notificacao ObterValido()
        {
            var notificacao = new Faker<Notificacao>("pt_BR")
             .CustomInstantiator(f => new Notificacao(
                 titulo: f.Random.String(),
                 corpo: f.Random.String(),
                 dispositivos: new List<Dispositivo> { DispositivoMocks.ObterValido() },
                 icone: f.Random.String(),
                 clickAction: f.Random.String()
                 ))
             .Generate();

            return notificacao;
        }

        public static Notificacao ObterComTitulo(string titulo)
        {
            var notificacao = new Faker<Notificacao>("pt_BR")
             .CustomInstantiator(f => new Notificacao(
                 titulo,
                 corpo: f.Random.String(),
                 dispositivos: new List<Dispositivo> { DispositivoMocks.ObterValido() },
                 icone: f.Random.String(),
                 clickAction: f.Random.String()
                 ))
             .Generate();

            return notificacao;
        }

        public static Notificacao ObterComCorpo(string corpo)
        {
            var notificacao = new Faker<Notificacao>("pt_BR")
             .CustomInstantiator(f => new Notificacao(
                 titulo: f.Random.String(),
                 corpo,
                 dispositivos: new List<Dispositivo> { DispositivoMocks.ObterValido() },
                 icone: f.Random.String(),
                 clickAction: f.Random.String()
                 ))
             .Generate();

            return notificacao;
        }

        public static Notificacao ObterComDispositivos(IEnumerable<Dispositivo> dispositivos)
        {
            var notificacao = new Faker<Notificacao>("pt_BR")
             .CustomInstantiator(f => new Notificacao(
                 titulo: f.Random.String(),
                 corpo: f.Random.String(),
                 dispositivos,
                 icone: f.Random.String(),
                 clickAction: f.Random.String()
                 ))
             .Generate();

            return notificacao;
        }

        public static Notificacao ObterComIcone(string icone)
        {
            var notificacao = new Faker<Notificacao>("pt_BR")
             .CustomInstantiator(f => new Notificacao(
                 titulo: f.Random.String(),
                 corpo: f.Random.String(),
                 dispositivos: new List<Dispositivo> { DispositivoMocks.ObterValido() },
                 icone,
                 clickAction: f.Random.String()
                 ))
             .Generate();

            return notificacao;
        }

        public static Notificacao ObterComClickAction(string clickAction)
        {
            var notificacao = new Faker<Notificacao>("pt_BR")
             .CustomInstantiator(f => new Notificacao(
                 titulo: f.Random.String(),
                 corpo: f.Random.String(),
                 dispositivos: new List<Dispositivo> { DispositivoMocks.ObterValido() },
                 icone: f.Random.String(),
                 clickAction
                 ))
             .Generate();

            return notificacao;
        }

    }

}
