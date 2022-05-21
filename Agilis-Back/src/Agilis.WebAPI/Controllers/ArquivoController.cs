using Agilis.Application.Services.Arquivos;
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
    public class ArquivoController : CrudController<ArquivoViewModel, ArquivoViewModel, Arquivo>
    {
        private readonly ArquivoCrudAppService _arquivoCrudAppService;

        public ArquivoController(ArquivoCrudAppService crudAppService) : base(crudAppService)
        {
            _arquivoCrudAppService = crudAppService;
        }

        [HttpGet]
        public ActionResult<ArquivoViewModel[]> Consultar()
        {
            var arquivosViewModel = _arquivoCrudAppService.ConsultarTodos();

            if (_arquivoCrudAppService.Invalido)
                return CustomBadRequest(_arquivoCrudAppService);

            return Ok(arquivosViewModel);
        }

        [AllowAnonymous]
        [HttpGet("{id}/download")]
        public async Task<IActionResult> DownloadNexo(Guid id)
        {
            var arquivo = await _arquivoCrudAppService.ConsultarPorIdAsync(id);
            if (arquivo == null)
                return NotFound();

            var byteArray = Convert.FromBase64String(arquivo.Conteudo.Split(',')[1]);

            return File(byteArray, MediaTypeNames.Application.Octet, arquivo.Nome);
        }
    }
}
