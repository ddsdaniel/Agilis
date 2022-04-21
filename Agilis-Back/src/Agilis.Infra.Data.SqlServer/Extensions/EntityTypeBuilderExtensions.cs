using Agilis.Core.Domain.Models.ValueObjects;
using Agilis.Core.Domain.Models.ValueObjects.Seguranca;
using Agilis.Core.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq.Expressions;

namespace Agilis.Infra.Data.SqlServer.Extensions
{
    public static class EntityTypeBuilderExtensions
    {
        private const string CHAVE_SECRETA = "3E07130D-008F-49E2-A96C-96C62E87A3E6";

        public static EntityTypeBuilder<TEntity> OwnsOneSenha<TEntity>(
            this EntityTypeBuilder<TEntity> builder,
            Expression<Func<TEntity, Senha>> navigationExpression)
            where TEntity : class
        {
            builder.OwnsOne(
                     navigationExpression,
                     navigationBuilder
                     =>
                     {
                         var algoritmoCriptografiaSimetrico = new AdvancedEncryptionStandardService();

                         navigationBuilder.Property(senha => senha.Conteudo)
                                          .HasColumnName(nameof(Senha))
                                          .HasMaxLength(256)//TODO: revisar se é necessário todo este tamanho
                                          .IsRequired()
                                          .HasConversion(
                                                senha => algoritmoCriptografiaSimetrico.Cifrar(senha, CHAVE_SECRETA),
                                                senha => algoritmoCriptografiaSimetrico.Decifrar(senha, CHAVE_SECRETA)
                                                );
                         
                         navigationBuilder.Ignore(senha => senha.Criticas);
                     });

            return builder;
        }
    }
}
