using Agilis.Core.Domain.Abstractions.Services;
using Agilis.Core.Domain.Models.ValueObjects.Seguranca;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Agilis.Infra.Data.Mongo.Serializers
{
    public class SenhaSerializer : SerializerBase<Senha>
    {
        private readonly ICriptografiaSimetrica _criptografiaSimetrica;
        private readonly string _segredo;

        public SenhaSerializer(ICriptografiaSimetrica criptografiaSimetrica, string segredo)
        {
            _criptografiaSimetrica = criptografiaSimetrica;
            _segredo = segredo;
        }

        public override Senha Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var senhaCrifrada = context.Reader.ReadString();
            var senhaDecifrada = _criptografiaSimetrica.Decifrar(senhaCrifrada, _segredo);
            if (_criptografiaSimetrica.Valido)
                return new Senha(senhaDecifrada);
            else
                return null;
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Senha senha)
        {
            var senhaCrifrada = _criptografiaSimetrica.Cifrar(senha.Conteudo, _segredo);
            if (_criptografiaSimetrica.Valido)
                context.Writer.WriteString(senhaCrifrada);
        }

    }
}
