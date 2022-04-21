import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { constantes } from 'src/app/consts/constantes';
import { Feature } from 'src/app/models/produtos/feature';
import { EpicoApiService } from 'src/app/services/apis/produtos/epico-api.service';
import { FeatureApiService } from 'src/app/services/apis/produtos/feature-api.service';
import { ComparadorService } from 'src/app/services/comparador.service';
import { TituloService } from 'src/app/services/titulo.service';
import { CrudFormComponent } from '../../crud/crud-form-component';


@Component({
  selector: 'app-features-form',
  templateUrl: './features-form.component.html',
  styleUrls: ['./features-form.component.scss']
})
export class FeaturesFormComponent extends CrudFormComponent<Feature> implements OnInit {


  constructor(
    router: Router,
    featureApiService: FeatureApiService,
    snackBar: MatSnackBar,
    tituloService: TituloService,
    private epicoApiService: EpicoApiService,
    protected activatedRoute: ActivatedRoute,
    public comparadorService: ComparadorService,
  ) {
    super(router, featureApiService, snackBar, activatedRoute, 'produtos/features');
    tituloService.definir('Cadastro da Feature');
  }

  sugerirNovo(): void {

    this.entidade = {
      id: constantes.newGuid,
      nome: '',
      epico: {
        id: constantes.newGuid,
        features: [],
        nome: '',
        produto: {
          id: constantes.newGuid,
          descricao: '',
          epicos: [],
          nome: '',
          urlRepositorio: '',
        }
      },
      tarefas: [],
    };

    this.activatedRoute.queryParams.subscribe({
      next: params => {

        if (params.produtoId && params.epicoId) {

          this.rotaPesquisa = `/produtos/${params.produtoId}/backlog`;

          this.epicoApiService.obterUm(params.epicoId)
            .subscribe({
              next: epico => this.entidade.epico = epico
            });
        }
      }
    });
  }
}
