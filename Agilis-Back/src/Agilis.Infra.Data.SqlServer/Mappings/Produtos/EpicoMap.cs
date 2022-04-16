using Agilis.Core.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agilis.Infra.Data.SqlServer.Mappings.Produtos
{
    public class EpicoMap : IEntityTypeConfiguration<Epico>
    {
        public void Configure(EntityTypeBuilder<Epico> builder)
        {
            builder.ToTable("Epicos");

            builder.Ignore(e => e.Criticas);

            builder.Property(e => e.Nome)
                .HasMaxLength(128);

            builder.HasMany(e => e.Features)
                .WithOne(f => f.Epico);
        }
    }
}
