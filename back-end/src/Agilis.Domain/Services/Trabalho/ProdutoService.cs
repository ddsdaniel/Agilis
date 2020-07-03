﻿using Agilis.Domain.Abstractions.Entities.Pessoas;
using Agilis.Domain.Abstractions.Repositories;
using Agilis.Domain.Abstractions.Services;
using Agilis.Domain.Abstractions.Services.Trabalho;
using Agilis.Domain.Models.Entities.Pessoas;
using Agilis.Domain.Models.Entities.Trabalho;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Agilis.Domain.Services.Trabalho
{
    public class ProdutoService : CrudService<Produto>, IProdutoService
    {

        public ProdutoService(IUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.ProdutoRepository)
        {
        }

        public ICollection<Produto> ConsultarTodos(IUsuario usuario)
        {
            var timesId = ObterTimesId(usuario).ToList();

            return _unitOfWork.ProdutoRepository
                   .AsQueryable()
                   .Where(p => timesId.Contains(p.TimeId))
                   .OrderBy(t => t.Nome)
                   .ToList();
        }

        public override ICollection<Produto> Pesquisar(string filtro)
          => _unitOfWork.ProdutoRepository
                 .AsQueryable()
                 .Where(t => t.Nome.ToLower().Contains(filtro.ToLower()))
                 .OrderBy(t => t.Nome)
                 .ToList();

        private IQueryable<Guid> ObterTimesId(IUsuario usuario)
        {
            return _unitOfWork.TimeRepository
                .ObterTimes(usuario)
                .Select(t => t.Id);
        }

        public ICollection<Produto> Pesquisar(string filtro, IUsuario usuario)
        {
            var timesId = ObterTimesId(usuario);

            return _unitOfWork.ProdutoRepository
                    .AsQueryable()
                    .Where(p => timesId.Contains(p.TimeId) && p.Nome.ToLower().Contains(filtro.ToLower()))
                    .OrderBy(t => t.Nome)
                    .ToList();
        }

        public ICollection<Produto> Pesquisar(string filtro, Guid timeId, IUsuario usuario)
        {
            var timesId = timeId == Guid.Empty
                ? ObterTimesId(usuario).ToArray()
                : new Guid[] { timeId };

            if (filtro == null)
                filtro = "";

            return _unitOfWork.ProdutoRepository
                    .AsQueryable()
                    .Where(p => timesId.Contains(p.TimeId) && p.Nome.ToLower().Contains(filtro.ToLower()))
                    .OrderBy(t => t.Nome)
                    .ToList();
        }

    }
}
