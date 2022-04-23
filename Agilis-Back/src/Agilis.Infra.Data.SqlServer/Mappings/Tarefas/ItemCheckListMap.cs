using Agilis.Core.Domain.Models.Entities.Tarefas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agilis.Infra.Data.SqlServer.Mappings.Tarefas
{
    public class ItemCheckListMap : IEntityTypeConfiguration<ItemCheckList>
    {
        public void Configure(EntityTypeBuilder<ItemCheckList> builder)
        {
            builder.ToTable("ItensCheckList");

            builder.Ignore(s => s.Criticas);

            builder.Property(s => s.Nome)
                .HasMaxLength(64);
        }
    }
}
