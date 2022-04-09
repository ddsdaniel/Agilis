using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Agilis.Core.Domain.Abstractions.Services;
using Agilis.Infra.Seguranca.Models.ValueObjects;

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
            if (_criptografiaSimetrica.Valid)
                return new Senha(senhaDecifrada);
            else
                return null;
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Senha senha)
        {
            var senhaCrifrada = _criptografiaSimetrica.Cifrar(senha.Conteudo, _segredo);
            if (_criptografiaSimetrica.Valid)
                context.Writer.WriteString(senhaCrifrada);
        }
        
    }
}
