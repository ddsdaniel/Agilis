using Agilis.Core.Domain.Abstractions.Services;
using Agilis.Core.Domain.Models.ValueObjects.Seguranca;
using Agilis.Infra.Data.Mongo.Serializers;
using MongoDB.Bson.Serialization;

namespace Agilis.Infra.Data.Mongo.Providers
{
    public class DomainProvider : IBsonSerializationProvider
    {
        private readonly ICriptografiaSimetrica _criptografiaSimetrica;
        private const string CHAVE_SECRETA = "3E07130D-008F-49E2-A96C-96C62E87A3E6";

        public DomainProvider(ICriptografiaSimetrica criptografiaSimetrica)
        {
            _criptografiaSimetrica = criptografiaSimetrica;
        }

        public IBsonSerializer GetSerializer(Type type)
        {
            if (type == typeof(Senha))
            {
                return new SenhaSerializer(_criptografiaSimetrica, CHAVE_SECRETA);
            }

            return null;
        }
    }
}
