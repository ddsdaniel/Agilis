using Agilis.Core.Domain.Models.Entities;
using AutoMapper;
using TrelloSharpEasy.Entities;

namespace Agilis.Infra.Importacao.Trello.AutoMapper
{
    public class SprintProfile : Profile
    {
        public SprintProfile()
        {
            CreateMap<Board, Sprint>()
                .ConvertUsing((board, context) =>
                    new Sprint(
                        nome: board.Name,
                        dataInicial: null,
                        dataFinal: null,
                        objetivos: String.Empty
                        )
                );
        }
    }
}
