using Agilis.Domain.Abstractions.Entities.Pessoas;
using Agilis.Domain.Abstractions.Repositories;
using Agilis.Domain.Abstractions.Services;
using Agilis.Domain.Abstractions.Services.Trabalho;
using Agilis.Domain.Models.Entities.Trabalho;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agilis.Domain.Services.Trabalho
{
    public class UserStoryService : CrudService<UserStory>, IUserStoryService
    {
        private readonly IUsuario _usuarioLogado;

        public UserStoryService(IUnitOfWork unitOfWork, IUsuario usuarioLogado)
            : base(unitOfWork, unitOfWork.UserStoryRepository)
        {
            _usuarioLogado = usuarioLogado;
        }

        public override ICollection<UserStory> Pesquisar(string filtro)
          => _unitOfWork.UserStoryRepository
                 .AsQueryable()
                 .Where(us => us.Nome.ToLower().Contains(filtro.ToLower()))
                 .OrderBy(us => us.Nome)
                 .ToList();

        public override async Task Atualizar(UserStory userStory)
        {
            await base.Atualizar(userStory);
            if (Valid)
            {
                var timesId = _unitOfWork.TimeRepository
                    .ObterTimes(_usuarioLogado)
                    .Select(t => t.Id)
                    .ToList();

                var achouUserStoryFK = false;
                var produtos = _unitOfWork.ProdutoRepository.ConsultarTodos(timesId);
                foreach (var produto in produtos)
                {
                    foreach (var tema in produto.StoryMapping.Temas)
                    {
                        foreach (var epico in tema.Epicos)
                        {
                            foreach (var us in epico.UserStories)
                            {
                                if (us.Id == userStory.Id)
                                {
                                    us.Nome = userStory.Nome;
                                    achouUserStoryFK = true;
                                    break;
                                }
                            }
                            if (achouUserStoryFK)
                                break;
                        }
                        if (achouUserStoryFK)
                            break;
                    }
                    if (achouUserStoryFK)
                    {
                        await _unitOfWork.ProdutoRepository.Atualizar(produto);
                        break;
                    }
                }
            }
        }
    }
}
