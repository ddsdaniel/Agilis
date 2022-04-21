using Agilis.Core.Domain.Models.Entities.Tarefas;
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

            builder.OwnsOne(
               tarefa => tarefa.HorasPrevistas,
               navigationBuilder =>
               {
                   navigationBuilder
                       .Property(hora => hora.Horario)
                       .HasColumnName("HorasPrevistas")
                       .HasMaxLength(5);

                   navigationBuilder.Ignore(email => email.Criticas);
               });


            builder.OwnsOne(
               tarefa => tarefa.HorasRealizadas,
               navigationBuilder =>
               {
                   navigationBuilder
                       .Property(hora => hora.Horario)
                       .HasColumnName("HorasRealizadas")
                       .HasMaxLength(5);

                   navigationBuilder.Ignore(email => email.Criticas);
               });
        }
    }
}
