using Agilis.Application.Services.Anexos;
using Agilis.Application.ViewModels;
using Agilis.Core.Domain.Models.Entities;
using Agilis.WebAPI.Abstractions.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Agilis.WebAPI.Controllers
{
    public class AnexoController : CrudController<AnexoViewModel, AnexoViewModel, Anexo>
    {
        private readonly AnexoCrudAppService _anexoCrudAppService;

        public AnexoController(AnexoCrudAppService crudAppService) : base(crudAppService)
        {
            _anexoCrudAppService = crudAppService;
        }

        [HttpGet]
        public ActionResult<AnexoViewModel[]> Consultar()
        {
            var anexosViewModel = _anexoCrudAppService.ConsultarTodos();

            if (_anexoCrudAppService.Invalido)
                return CustomBadRequest(_anexoCrudAppService);

            return Ok(anexosViewModel);
        }

        [AllowAnonymous]
        [HttpGet("{id}/download")]
        public async Task<IActionResult> DownloadAnexo(Guid id)
        {
            var anexo = await _anexoCrudAppService.ConsultarPorIdAsync(id);
            if (anexo == null)
                return NotFound();

            if (anexo.Tipo == Core.Domain.Enums.TipoAnexo.Link)
            {
                Response.Redirect(anexo.Conteudo);
                return Ok();
            }

            var byteArray = Convert.FromBase64String(anexo.Conteudo.Split(',')[1]);

            return File(byteArray, MediaTypeNames.Application.Octet, anexo.Nome);
        }
    }
}
