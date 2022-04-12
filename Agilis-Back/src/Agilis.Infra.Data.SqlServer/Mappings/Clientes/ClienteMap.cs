using Agilis.Core.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agilis.Infra.Data.SqlServer.Mappings.Clientes
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");

            builder.Ignore(u => u.Criticas);

            builder.Property(u => u.Nome)
                .HasMaxLength(128);

            builder.Property(u => u.IdIntegracaoSac)
                .HasMaxLength(16);
        }
    }
}
