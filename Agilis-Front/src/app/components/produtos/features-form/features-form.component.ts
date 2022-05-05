import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/internal/operators/tap';
import { constantes } from 'src/app/consts/constantes';
import { Feature } from 'src/app/models/produtos/feature';
import { Produto } from 'src/app/models/produtos/produto';
import { FeatureApiService } from 'src/app/services/apis/produtos/feature-api.service';
import { ProdutoApiService } from 'src/app/services/apis/produtos/produto-api.service';
import { ComparadorService } from 'src/app/services/comparador.service';
import { TituloService } from 'src/app/services/titulo.service';

import { CrudFormComponent } from '../../crud/crud-form-component';


@Component({
  selector: 'app-features-form',
  templateUrl: './features-form.component.html',
  styleUrls: ['./features-form.component.scss']
})
export class FeaturesFormComponent extends CrudFormComponent<Feature> implements OnInit {

  produtos: Produto[] = [];


  constructor(
    router: Router,
    featureApiService: FeatureApiService,
    snackBar: MatSnackBar,
    tituloService: TituloService,
    private produtoApiService: ProdutoApiService,
    protected activatedRoute: ActivatedRoute,
    public comparadorService: ComparadorService,
  ) {
    super(router, featureApiService, snackBar, activatedRoute, 'produtos/features');
    tituloService.definir('Cadastro da Feature');
  }

  carregarDependencias(): Observable<void> {
    return this.obterProdutos();
  }

  public obterProdutos(): Observable<any> {
    return this.produtoApiService.obterTodos()
      .pipe(
        tap(produtos => this.produtos = produtos)
      );
  }

  sugerirNovo(): void {

    this.entidade = {
      id: constantes.newGuid,
      nome: '',
      produto: null,
    };
  }
}

