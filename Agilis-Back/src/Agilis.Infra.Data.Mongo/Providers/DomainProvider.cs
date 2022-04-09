using MongoDB.Bson.Serialization;
using System;
using Agilis.Core.Domain.Abstractions.Services;
using Agilis.Infra.Data.Mongo.Serializers;
using Agilis.Infra.Seguranca.Models.ValueObjects;

namespace Agilis.Infra.Data.Mongo.Providers
{
    public class DomainProvider : IBsonSerializationProvider
    {
        private readonly ICriptografiaSimetrica _criptografiaSimetrica;
        private readonly string _segredo;

        public DomainProvider(ICriptografiaSimetrica criptografiaSimetrica, string segredo)
        {
            _criptografiaSimetrica = criptografiaSimetrica;
            _segredo = segredo;
        }

        public IBsonSerializer GetSerializer(Type type)
        {
            if (type == typeof(Senha))
            {
                return new SenhaSerializer(_criptografiaSimetrica, _segredo);
            }

            return null;
        }
    }
}
