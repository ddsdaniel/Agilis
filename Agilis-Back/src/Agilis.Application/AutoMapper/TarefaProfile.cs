﻿using Agilis.Application.ViewModels.Tarefas;
using Agilis.Core.Domain.Models.Entities;
using AutoMapper;

namespace Agilis.Application.AutoMapper
{
    public class TarefaProfile : Profile
    {
        public TarefaProfile()
        {
            CreateMap<Tarefa, TarefaViewModel>()
                .ReverseMap();
        }
    }
}
