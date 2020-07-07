import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { CrudComponent } from 'src/app/components/crud-component';
import { Produto } from 'src/app/models/trabalho/produtos/produto';
import { Tema } from 'src/app/models/trabalho/temas/tema';
import { ProdutosApiService } from 'src/app/services/api/trabalho/produtos-api.service';
import { TemasApiService } from 'src/app/services/api/trabalho/temas-api.service';
import { constantes } from 'src/app/constants/constantes';
import { EscopoProduto } from 'src/app/enums/pessoas/escopo-produto.enum';

@Component({
  selector: 'app-temas',
  templateUrl: './temas.component.html',
  styleUrls: ['./temas.component.scss']
})
export class TemasComponent extends CrudComponent<Tema> {

  produtoId: string = constantes.newGuid;
  produtos: Produto[];

  constructor(
    public temasApiService: TemasApiService,
    public snackBar: MatSnackBar,
    router: Router,
    private produtosApiService: ProdutosApiService,
  ) {
    super(temasApiService, snackBar, router, 'temas');
    this.carregarProdutos();
  }

  carregarProdutos() {
    this.produtosApiService.obterTodos()
      .subscribe(
        (produtos) => {
          const todos: Produto = {
            id: constantes.newGuid,
            nome: 'Todos',
            timeId: constantes.newGuid,
            temas: [],
            atores: [],
          };
          produtos.insert(0, todos);
          this.produtos = produtos;
        },
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

  atualizarDados() {
    this.temasApiService.pesquisarPorProduto(this.filtro, this.produtoId)
      .subscribe(
        (lista: Tema[]) => this.lista = lista,
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

}
