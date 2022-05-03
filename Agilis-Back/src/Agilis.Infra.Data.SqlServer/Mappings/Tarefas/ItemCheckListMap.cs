using Agilis.Core.Domain.Models.Entities.Tarefas;
using Agilis.Infra.Data.SqlServer.Extensions;
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
                .HasMaxLength(256);

            builder.OwnsOneHora(item => item.HorasPrevistas, "HorasPrevistas");
        }
    }
}
