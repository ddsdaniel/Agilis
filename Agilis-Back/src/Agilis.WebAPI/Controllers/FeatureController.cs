﻿using Agilis.WebAPI.Abstractions.Controllers;
using Microsoft.AspNetCore.Mvc;
using Agilis.Core.Domain.Models.Entities;
using Agilis.Application.ViewModels.Produtos;
using Agilis.Application.Services.Produtos;

namespace Agilis.WebAPI.Controllers
{
    public class FeatureController : CrudController<FeatureViewModel, FeatureViewModel, Feature>
    {
        private readonly FeatureCrudAppService _featureCrudAppService;

        public FeatureController(FeatureCrudAppService crudAppService) : base(crudAppService)
        {
            _featureCrudAppService = crudAppService;
        }

        [HttpGet]
        public ActionResult<FeatureViewModel[]> Consultar()
        {
            var featuresViewModel = _featureCrudAppService.ConsultarTodos();

            if (_featureCrudAppService.Invalido)
                return CustomBadRequest(_featureCrudAppService);

            return Ok(featuresViewModel);
        }
    }
}
