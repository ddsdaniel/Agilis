import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { CrudComponent } from 'src/app/components/crud-component';
import { constantes } from 'src/app/constants/constantes';
import { Ator } from 'src/app/models/pessoas/ator';
import { Produto } from 'src/app/models/trabalho/produtos/produto';
import { AtoresApiService } from 'src/app/services/api/pessoas/atores-api.service';
import { ProdutosApiService } from 'src/app/services/api/trabalho/produtos-api.service';

@Component({
  selector: 'app-atores',
  templateUrl: './atores.component.html',
  styleUrls: ['./atores.component.scss']
})
export class AtoresComponent extends CrudComponent<Ator> {

  produtoId: string = constantes.newGuid;
  produtos: Produto[];

  constructor(
    public atoresApiService: AtoresApiService,
    public snackBar: MatSnackBar,
    router: Router,
    private produtosApiService: ProdutosApiService,
  ) {
    super(atoresApiService, snackBar, router, 'atores');
    this.carregarProdutos();
  }

  carregarProdutos() {
    this.produtosApiService.obterTodos()
      .subscribe(
        (produtos) => {
          const todos: Produto = {
            id: constantes.newGuid,
            nome: 'Todos',
            timeId: constantes.newGuid
          };
          produtos.insert(0, todos);
          this.produtos = produtos;
        },
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

  atualizarDados() {
    this.atoresApiService.pesquisarPorProduto(this.filtro, this.produtoId)
      .subscribe(
        (lista: Ator[]) => this.lista = lista,
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

}
