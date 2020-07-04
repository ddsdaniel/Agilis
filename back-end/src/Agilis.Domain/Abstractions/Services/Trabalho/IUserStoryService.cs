using Agilis.Domain.Models.Entities.Trabalho;
using System.Collections.Generic;
using System;
using Agilis.Domain.Abstractions.Entities.Pessoas;

namespace Agilis.Domain.Abstractions.Services.Trabalho
{
    public interface IUserStoryService : ICrudSeguroService<UserStory>
    {
        ICollection<UserStory> Pesquisar(string filtro, Guid epicoId, IUsuario usuario);
    }
}
