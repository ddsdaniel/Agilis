using Agilis.Core.Domain.Models.Entities.Tarefas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agilis.Infra.Data.SqlServer.Mappings.Tarefas
{
    public class TagTarefaMap: IEntityTypeConfiguration<TagTarefa>
    {
        public void Configure(EntityTypeBuilder<TagTarefa> builder)
        {
            builder.ToTable("TagTarefas");
            builder.HasKey(tt => new { tt.TarefaId, tt.TagId});
        }
    }
}

