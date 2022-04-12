using Agilis.Core.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agilis.Infra.Data.SqlServer.Mappings.Produtos
{
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produtos");

            builder.Ignore(u => u.Criticas);

            builder.Property(u => u.Nome)
                .HasMaxLength(64);

            builder.Property(u => u.UrlRepositorio)
                .HasMaxLength(256);

            builder.Property(u => u.Descricao);
        }
    }
}
