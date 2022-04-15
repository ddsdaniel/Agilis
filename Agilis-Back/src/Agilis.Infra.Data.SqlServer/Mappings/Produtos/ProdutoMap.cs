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

            builder.Ignore(p => p.Criticas);

            builder.Property(p => p.Nome)
                .HasMaxLength(64);

            builder.Property(p => p.UrlRepositorio)
                .HasMaxLength(256);

            builder.Property(p => p.Descricao);

            builder.HasMany(p => p.Backlog)
                .WithOne(t => t.Produto);
        }
    }
}
