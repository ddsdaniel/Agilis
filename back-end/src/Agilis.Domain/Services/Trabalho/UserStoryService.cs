using Agilis.Domain.Abstractions.Entities.Pessoas;
using Agilis.Domain.Abstractions.Repositories;
using Agilis.Domain.Abstractions.Services;
using Agilis.Domain.Abstractions.Services.Trabalho;
using Agilis.Domain.Models.Entities.Trabalho;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Agilis.Domain.Services.Trabalho
{
    public class UserStoryService : CrudService<UserStory>, IUserStoryService
    {
        public UserStoryService(IUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.UserStoryRepository)
        {
        }

        public ICollection<UserStory> ConsultarTodos(IUsuario usuario)
        {
            var epicosId = ObterEpicosDoUsuario(usuario).ToList();

            return _unitOfWork.UserStoryRepository
                   .AsQueryable()
                   .Where(us => epicosId.Contains(us.EpicoId))
                   .OrderBy(us => us.Nome)
                   .ToList();
        }

        public override ICollection<UserStory> Pesquisar(string filtro)
          => _unitOfWork.UserStoryRepository
                 .AsQueryable()
                 .Where(us => us.Nome.ToLower().Contains(filtro.ToLower()))
                 .OrderBy(us => us.Nome)
                 .ToList();

        public ICollection<UserStory> Pesquisar(string filtro, IUsuario usuario)
        {
            var epicosId = ObterEpicosDoUsuario(usuario);

            return _unitOfWork.UserStoryRepository
                    .AsQueryable()
                    .Where(us => epicosId.Contains(us.EpicoId) && us.Nome.ToLower().Contains(filtro.ToLower()))
                    .OrderBy(us => us.Nome)
                    .ToList();
        }

        public ICollection<UserStory> Pesquisar(string filtro, Guid epicoId, IUsuario usuario)
        {
            var epicosId = epicoId == Guid.Empty
                ? ObterEpicosDoUsuario(usuario)
                : new List<Guid> { epicoId };

            if (filtro == null)
                filtro = "";

            return _unitOfWork.UserStoryRepository
                    .AsQueryable()
                    .Where(us => epicosId.Contains(us.EpicoId) && us.Nome.ToLower().Contains(filtro.ToLower()))
                    .OrderBy(us => us.Nome)
                    .ToList();
        }

        private List<Guid> ObterEpicosDoUsuario(IUsuario usuario)
        {
            var timesDoUsuario = _unitOfWork.TimeRepository
               .ObterTimes(usuario)
               .Select(t => t.Id)
               .ToList();

            var produtosId = _unitOfWork.ProdutoRepository
                .AsQueryable()
                .Where(p => timesDoUsuario.Contains(p.TimeId))
                .Select(p => p.Id)
                .ToList();

            var temasId = _unitOfWork.TemaRepository
                .AsQueryable()
                .Where(t => produtosId.Contains(t.ProdutoId))
                .Select(t => t.Id)
                .ToList();

            var epicosId = _unitOfWork.EpicoRepository
                .AsQueryable()
                .Where(e => temasId.Contains(e.TemaId))
                .Select(e => e.Id)
                .ToList();

            return epicosId;
        }
    }
}
