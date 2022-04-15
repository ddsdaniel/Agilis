import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { constantes } from 'src/app/consts/constantes';
import { Produto } from 'src/app/models/produto';
import { Tarefa } from 'src/app/models/tarefa';
import { ProdutoApiService } from 'src/app/services/apis/produto-api.service';
import { TarefaApiService } from 'src/app/services/apis/tarefa-api.service';
import { ComparadorService } from 'src/app/services/comparador.service';
import { TituloService } from 'src/app/services/titulo.service';
import { CrudFormComponent } from '../../crud/crud-form-component';

@Component({
  selector: 'app-tarefas-form',
  templateUrl: './tarefas-form.component.html',
  styleUrls: ['./tarefas-form.component.scss']
})
export class TarefasFormComponent extends CrudFormComponent<Tarefa> implements OnInit {

  produtos: Produto[] = [];

  constructor(
    private produtoApiService: ProdutoApiService,
    router: Router,
    tarefaApiService: TarefaApiService,
    snackBar: MatSnackBar,
    tituloService: TituloService,
    protected activatedRoute: ActivatedRoute,
    public comparadorService: ComparadorService,
  ) {
    super(router, tarefaApiService, snackBar, activatedRoute, 'tarefas');
    tituloService.definir('Cadastro da Tarefa');
  }

  ngOnInit(): void {
    this.produtoApiService.obterTodos()
      .subscribe({
        next: produtos => {
          this.produtos = produtos;
          super.ngOnInit();
        }
      });

  }

  sugerirNovo(): void {
    this.entidade = {
      id: constantes.newGuid,
      titulo: '',
      descricao: '',
      produtoId: constantes.newGuid
    };
  }

  salvar() {
    if (this.entidade.produto) {
      this.entidade.produtoId = this.entidade.produto.id;
    }
    super.salvar();
  }

}
