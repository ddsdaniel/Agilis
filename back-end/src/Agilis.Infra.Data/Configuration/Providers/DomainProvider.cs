using DDS.Domain.Core.Models.ValueObjects.Seguranca.Senhas;
using MongoDB.Bson.Serialization;
using Agilis.Infra.Data.Configuration.Serializers;
using System;
using DDS.Domain.Core.Abstractions.Services.Seguranca.Criptografia;
using Agilis.Domain.Abstractions.ValueObjects;

namespace Agilis.Infra.Data.Configuration.Providers
{
    public class DomainProvider : IBsonSerializationProvider
    {
        private readonly ICriptografiaSimetrica _criptografiaSimetrica;
        private readonly IAppSettings _appSettings;

        public DomainProvider(ICriptografiaSimetrica criptografiaSimetrica, IAppSettings appSettings)
        {
            _criptografiaSimetrica = criptografiaSimetrica;
            _appSettings = appSettings;
        }

        public IBsonSerializer GetSerializer(Type type)
        {
            if (type == typeof(SenhaMedia))
            {
                return new SenhaMediaSerializer(_criptografiaSimetrica, _appSettings);
            }

            return null;
        }
    }
}
