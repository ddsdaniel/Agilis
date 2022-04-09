using Flunt.Notifications;
using Agilis.Core.Domain.Abstractions.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agilis.Application.Abstractions.Services;

namespace Agilis.WebAPI.Abstractions.Controllers
{
    public abstract class CrudController<
        TViewModelCadastro,
        TViewModelConsulta,
        TEntity        
        > : ConsultaController<TViewModelConsulta, TEntity>
        where TEntity : Entidade
        where TViewModelCadastro : class
        where TViewModelConsulta : class
    {
        private readonly CrudAppService<TViewModelCadastro, TViewModelConsulta, TEntity> _crudAppService;

        public CrudController(CrudAppService<TViewModelCadastro, TViewModelConsulta, TEntity> crudAppService)
            :base(crudAppService)
        {
            _crudAppService = crudAppService;
        }

        /// <summary>
        /// Cadastra a entidade no repositório
        /// </summary>
        /// <param name="novaEntidadeViewModel">Dados da nova entidade</param>
        /// <returns>View model atualizada recém cadastrada</returns>
        [HttpPost]
        public virtual async Task<ActionResult> Post(TViewModelCadastro novaEntidadeViewModel)
        {
            var viewModelConsulta = await _crudAppService.AdicionarAsync(novaEntidadeViewModel);

            if (_crudAppService.Invalid)
                return CustomBadRequest(_crudAppService);

            return Ok(viewModelConsulta);
        }

        /// <summary>
        /// Altera os dados de uma entidade do repositório
        /// </summary>
        /// <param name="id">Id da entidade que será alterada</param>
        /// <param name="viewModelCadastro">Dados que serão alterados</param>
        /// <returns>OK (201)</returns>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Notification>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<Notification>), StatusCodes.Status400BadRequest)]
        public virtual async Task<ActionResult> Put(Guid id, TViewModelCadastro viewModelCadastro)
        {
            await _crudAppService.AlterarAsync(id, viewModelCadastro);

            if (_crudAppService.Invalid)
                return CustomBadRequest(_crudAppService);

            return Ok();
        }

        /// <summary>
        /// Exclui uma entidade do repositório
        /// </summary>
        /// <param name="id">Id da entidade que será excluída</param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Notification>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<Notification>), StatusCodes.Status400BadRequest)]
        public virtual async Task<ActionResult> Delete(Guid id)
        {
            await _crudAppService.ExcluirAsync(id);

            if (_crudAppService.Invalid)
                return CustomBadRequest(_crudAppService);            

            return Ok();
        }
    }
}
