import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { CrudFormComponent } from 'src/app/components/crud-form-component';
import { constantes } from 'src/app/constants/constantes';
import { Produto } from 'src/app/models/trabalho/produtos/produto';
import { Tema } from 'src/app/models/trabalho/temas/tema';
import { ProdutosApiService } from 'src/app/services/api/trabalho/produtos-api.service';
import { TemasApiService } from 'src/app/services/api/trabalho/temas-api.service';

@Component({
  selector: 'app-temas-form',
  templateUrl: './temas-form.component.html',
  styleUrls: ['./temas-form.component.scss']
})
export class TemasFormComponent extends CrudFormComponent<Tema> {

  produtos: Produto[];
  temaApiService: TemasApiService;
  sugestaoProdutoId: string = constantes.newGuid;

  constructor(
    router: Router,
    temaApiService: TemasApiService,
    snackBar: MatSnackBar,
    protected activatedRoute: ActivatedRoute,
    private produtosApiService: ProdutosApiService,
  ) {
    super(router, temaApiService, snackBar, activatedRoute, 'temas');
    this.recuperarQueryParams();
    this.temaApiService = temaApiService;
    this.carregarProdutos();
    this.sugerirNovo();
  }

  recuperarQueryParams() {
    this.activatedRoute.queryParams
      .subscribe(params => {
        this.sugestaoProdutoId = params.produtoId;
      });
  }

  carregarProdutos() {
    this.produtosApiService.obterTodos()
      .subscribe(
        (produtos) => this.produtos = produtos,
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

  sugerirNovo() {
    this.entidade = {
      id: constantes.newGuid,
      nome: '',
      produtoId: this.sugestaoProdutoId
    };
  }
}
