using Agilis.Core.Domain.Models.Entities.Seguranca;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agilis.Infra.Data.SqlServer.Mappings.Seguranca
{
    public class RefreshTokenMap : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshTokens");

            builder.Ignore(u => u.Criticas);

            builder.Property(u => u.Token)
                .HasMaxLength(256);
        }
    }
}
