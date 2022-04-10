using Agilis.Core.Domain.Models.Entities.Seguranca;
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

            builder.OwnsOneSenha(usuario => usuario.Senha);
        }
    }
}
