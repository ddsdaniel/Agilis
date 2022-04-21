using Agilis.Core.Domain.Models.Entities;
using Agilis.Infra.Data.SqlServer.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agilis.Infra.Data.SqlServer.Mappings.Tarefas
{
    public class TarefaMap : IEntityTypeConfiguration<Tarefa>
    {
        public void Configure(EntityTypeBuilder<Tarefa> builder)
        {
            builder.ToTable("Tarefas");

            builder.Ignore(u => u.Criticas);

            builder.Property(u => u.Titulo)
                .HasMaxLength(256);

            builder.OwnsOneHora(t => t.HorasPrevistas, "HorasPrevistas");

            builder.OwnsOneHora(t => t.HorasRealizadas, "HorasRealizadas");
        }
    }
}
