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

                var userStoryFK = _unitOfWork.ProdutoRepository
                    .ConsultarTodos(timesId)
                    .SelectMany(p => p.StoryMapping.Temas)
                    .SelectMany(t => t.Epicos)
                    .SelectMany(e => e.UserStories)
                    .FirstOrDefault(us => us.Id == userStory.Id);

                if (userStoryFK != null)
                {
                    userStoryFK.Nome = userStory.Nome;

                    var produto = _unitOfWork.ProdutoRepository
                        .ConsultarTodos(timesId)
                        .FirstOrDefault(p => p.StoryMapping.Temas.Any(t => t.Epicos.Any(e => e.UserStories.Any(us => us.Id == userStory.Id))));

                    await _unitOfWork.ProdutoRepository.Atualizar(produto);
                }
            }
        }
    }
}
