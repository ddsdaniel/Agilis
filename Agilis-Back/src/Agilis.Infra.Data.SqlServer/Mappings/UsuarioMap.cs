using Agilis.Core.Domain.Models.Entities.Seguranca;
using Agilis.Core.Domain.Models.ValueObjects;
using Agilis.Infra.Data.SqlServer.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agilis.Infra.Data.SqlServer.Mappings
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");

            builder.Ignore(u => u.Criticas);

            builder.Property(u => u.Nome)
                .HasMaxLength(64);

            builder.Property(u => u.Sobrenome)
                .HasMaxLength(128);

            builder.OwnsOneSenha(usuario => usuario.Senha);

            builder.OwnsOne(
                usuario => usuario.Email,
                navigationBuilder =>
                {
                    navigationBuilder
                        .Property(email => email.Endereco)
                        .HasColumnName(nameof(Email))
                        .HasMaxLength(256);

                    navigationBuilder.Ignore(email => email.Criticas);
                });
        }
    }
}
