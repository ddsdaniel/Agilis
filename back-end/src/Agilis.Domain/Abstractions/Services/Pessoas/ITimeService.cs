﻿using DDS.Domain.Core.Abstractions.Services;
using Agilis.Domain.Models.Entities.Pessoas;
using System.Collections.Generic;
using Agilis.Domain.Abstractions.Entities.Pessoas;

namespace Agilis.Domain.Abstractions.Services.Pessoas
{
    public interface ITimeService : ICrudService<Time>
    {
        ICollection<Time> ConsultarTodos(IUsuario usuario);
        ICollection<Time> Pesquisar(string filtro, IUsuario usuario);
    }
}
