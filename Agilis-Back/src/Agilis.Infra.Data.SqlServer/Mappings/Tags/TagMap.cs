using Agilis.Core.Domain.Models.Entities.Tags;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agilis.Infra.Data.SqlServer.Mappings.Tags
{
    public class TagMap : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("Tags");

            builder.Ignore(s => s.Criticas);

            builder.Property(s => s.Nome)
                .HasMaxLength(64);

            builder.OwnsOne(
              tag => tag.Cor,
              navigationBuilder =>
              {
                  navigationBuilder
                      .Property(cor => cor.Codigo)
                      .HasColumnName("Cor")
                      .HasMaxLength(7);

                  navigationBuilder.Ignore(email => email.Criticas);
              });
        }
    }
}
