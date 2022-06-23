import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Produto } from 'src/app/models/produtos/produto';
import { ImportacaoTrello } from 'src/app/models/tarefas/importacao-trello';
import { ProdutoApiService } from 'src/app/services/apis/produtos/produto-api.service';
import { TrelloApiService } from 'src/app/services/apis/trello-api.service';
import { ComparadorService } from 'src/app/services/comparador.service';

@Component({
  selector: 'app-importacao-trello',
  templateUrl: './importacao-trello.component.html',
  styleUrls: ['./importacao-trello.component.scss']
})
export class ImportacaoTrelloComponent implements OnInit {

  produtos: Produto[] = [];

  importacaoTrello: ImportacaoTrello = {
    boardId: '',
    limparDados: false,
    produto: null
  };

  constructor(
    private produtoApiService: ProdutoApiService,
    private trelloApiService: TrelloApiService,
    private matSnackBar: MatSnackBar,
    public comparadorService: ComparadorService,
  ) { }

  ngOnInit(): void {
    this.produtoApiService.obterTodos()
      .subscribe({
        next: produtos => this.produtos = produtos,
        error: (error: HttpErrorResponse) => this.matSnackBar.open(error.message)
      });
  }

  importar() {
    this.trelloApiService.importar(this.importacaoTrello)
      .subscribe({
        next: _ => this.matSnackBar.open('Importação concluída com sucesso'),
        error: (error: HttpErrorResponse) => this.matSnackBar.open(error.message)
      });
  }

}
