using Agilis.Core.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agilis.Infra.Data.SqlServer.Mappings.Produtos
{
    public class FeatureMap : IEntityTypeConfiguration<Feature>
    {
        public void Configure(EntityTypeBuilder<Feature> builder)
        {
            builder.ToTable("Features");

            builder.Ignore(e => e.Criticas);

            builder.Property(e => e.Nome)
                .HasMaxLength(64);
        }
    }
}
