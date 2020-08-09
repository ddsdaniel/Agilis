import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { CrudFormComponent } from 'src/app/components/crud-form-component';
import { constantes } from 'src/app/constants/constantes';
import { Produto } from 'src/app/models/trabalho/produtos/produto';
import { Ator } from 'src/app/models/pessoas/ator';
import { ProdutosApiService } from 'src/app/services/api/trabalho/produtos-api.service';
import { AtoresApiService } from 'src/app/services/api/pessoas/atores-api.service';

@Component({
  selector: 'app-atores-form',
  templateUrl: './atores-form.component.html',
  styleUrls: ['./atores-form.component.scss']
})
export class AtoresFormComponent extends CrudFormComponent<Ator> {

  sugestaoProdutoId: string = constantes.newGuid;
  produtos: Produto[];
  atorApiService: AtoresApiService;

  constructor(
    router: Router,
    atorApiService: AtoresApiService,
    snackBar: MatSnackBar,
    protected activatedRoute: ActivatedRoute,
    private produtosApiService: ProdutosApiService,
  ) {
    super(router, atorApiService, snackBar, activatedRoute, 'atores');
    this.recuperarQueryParams();
    this.atorApiService = atorApiService;
    this.carregarProdutos();
    this.sugerirNovo();
  }

  recuperarQueryParams() {
    this.activatedRoute.queryParams
      .subscribe(params => {
        if (params.produtoId) {
          this.sugestaoProdutoId = params.produtoId;
          super.rotaPesquisa = 'produtos/' + this.sugestaoProdutoId;
        }
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
