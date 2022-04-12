using Agilis.Core.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agilis.Infra.Data.SqlServer.Mappings.Times
{
    public class TimeMap : IEntityTypeConfiguration<Time>
    {
        public void Configure(EntityTypeBuilder<Time> builder)
        {
            builder.ToTable("Times");

            builder.Ignore(u => u.Criticas);

            builder.Property(u => u.Nome)
                .HasMaxLength(32);
        }
    }
}
