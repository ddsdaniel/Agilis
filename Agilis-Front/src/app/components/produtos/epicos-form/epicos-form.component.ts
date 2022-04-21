import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { constantes } from 'src/app/consts/constantes';
import { Epico } from 'src/app/models/produtos/epico';
import { EpicoApiService } from 'src/app/services/apis/produtos/epico-api.service';
import { ProdutoApiService } from 'src/app/services/apis/produtos/produto-api.service';
import { ComparadorService } from 'src/app/services/comparador.service';
import { TituloService } from 'src/app/services/titulo.service';
import { CrudFormComponent } from '../../crud/crud-form-component';

@Component({
  selector: 'app-epicos-form',
  templateUrl: './epicos-form.component.html',
  styleUrls: ['./epicos-form.component.scss']
})
export class EpicosFormComponent extends CrudFormComponent<Epico> implements OnInit {


  constructor(
    router: Router,
    epicoApiService: EpicoApiService,
    snackBar: MatSnackBar,
    tituloService: TituloService,
    private produtoApiService: ProdutoApiService,
    protected activatedRoute: ActivatedRoute,
    public comparadorService: ComparadorService,
  ) {
    super(router, epicoApiService, snackBar, activatedRoute, 'produtos/epicos');
    tituloService.definir('Cadastro do Épico');
  }

  sugerirNovo(): void {

    this.entidade = {
      id: constantes.newGuid,
      nome: '',
      features: [],
      produto: {
        id: constantes.newGuid,
        nome: '',
        descricao: '',
        epicos: [],
        urlRepositorio: '',
      }
    };

    this.activatedRoute.queryParams.subscribe({
      next: params => {
        if (params.produtoId) {

          this.rotaPesquisa = `/produtos/${params.produtoId}/backlog`;

          this.produtoApiService.obterUm(params.produtoId)
            .subscribe({
              next: produto => this.entidade.produto = produto
            });
        }
      }
    });
  }

}
