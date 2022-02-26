using DDS.Domain.Core.Abstractions.Services.Seguranca.Criptografia;
using DDS.Domain.Core.Models.ValueObjects.Seguranca.Senhas;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Agilis.Domain.Abstractions.ValueObjects;
using Agilis.Domain.Models.Entities.Pessoas;

namespace Agilis.Infra.Data.Configuration.Serializers
{
    public class SenhaMediaSerializer : SerializerBase<SenhaMedia>
    {
        private readonly ICriptografiaSimetrica _criptografiaSimetrica;
        private readonly IAppSettings _appSettings;
        

        public SenhaMediaSerializer(ICriptografiaSimetrica criptografiaSimetrica, IAppSettings appSettings)
        {
            _criptografiaSimetrica = criptografiaSimetrica;
            _appSettings = appSettings;
        }

        public override SenhaMedia Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var senhaCrifrada = context.Reader.ReadString();
            var senhaDecifrada = _criptografiaSimetrica.Decifrar(senhaCrifrada, _appSettings.Segredo);
            if (_criptografiaSimetrica.Valid)
                return new SenhaMedia(senhaDecifrada, Usuario.TAMANHO_MINIMO_SENHA);
            else
                return null;
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, SenhaMedia senha)
        {
            var senhaCrifrada = _criptografiaSimetrica.Cifrar(senha.Conteudo, _appSettings.Segredo);
            if (_criptografiaSimetrica.Valid)
                context.Writer.WriteString(senhaCrifrada);
        }
        
    }
}
