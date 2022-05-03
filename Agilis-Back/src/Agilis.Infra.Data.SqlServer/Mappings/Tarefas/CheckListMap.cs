using Agilis.Core.Domain.Models.Entities.Tarefas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agilis.Infra.Data.SqlServer.Mappings.Tarefas
{
    public class CheckListMap : IEntityTypeConfiguration<CheckList>
    {
        public void Configure(EntityTypeBuilder<CheckList> builder)
        {
            builder.ToTable("CheckLists");

            builder.Ignore(s => s.Criticas);

            builder.Property(s => s.Nome)
                .HasMaxLength(64);
        }
    }
}
