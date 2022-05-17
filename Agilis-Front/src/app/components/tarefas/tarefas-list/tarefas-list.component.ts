import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Observable } from 'rxjs/internal/Observable';
import { tap } from 'rxjs/internal/operators/tap';
import { constantes } from 'src/app/consts/constantes';
import { BottomSheetItem } from 'src/app/models/bottom-sheet-item';
import { Sprint } from 'src/app/models/sprint';
import { FiltroTarefa } from 'src/app/models/tarefas/filtro-tarefa';
import { Tarefa } from 'src/app/models/tarefas/tarefa';
import { SprintApiService } from 'src/app/services/apis/sprint-api.service';
import { TarefaApiService } from 'src/app/services/apis/tarefa-api.service';
import { BottomSheetService } from 'src/app/services/bottom-sheet.service';
import { TituloService } from 'src/app/services/titulo.service';

import { CrudListComponent } from '../../crud/crud-list-component';
import { BottomSheetComponent } from '../../widgets/bottom-sheet/bottom-sheet.component';

@Component({
  selector: 'app-tarefas-list',
  templateUrl: './tarefas-list.component.html',
  styleUrls: ['./tarefas-list.component.scss']
})
export class TarefasListComponent extends CrudListComponent<Tarefa> implements OnInit {

  // Filtros
  sprints: Sprint[] = [];

  filtros: FiltroTarefa = {
    sprintId: ''
  };

  constructor(
    private sprintApiService: SprintApiService,
    private bottomSheetService: BottomSheetService,
    router: Router,
    tituloService: TituloService,
    public snackBar: MatSnackBar,
    public tarefaApiService: TarefaApiService,
  ) {
    super(tarefaApiService, snackBar, router, 'tarefas');

    tituloService.definir('Tarefas');
  }

  ngOnInit(): void {
    this.carregarCampos()
      .subscribe({
        next: _ => super.ngOnInit()
      });
  }

  carregarCampos(): Observable<any> {
    return this.sprintApiService.obterTodos()
      .pipe(
        tap(sprints => this.sprints = sprints)
      );
  }

  onPesquisar(criterio: string) {
    if (criterio) {
      this.lista = (this.listaCompleta || [])
        .filter(item => new RegExp(criterio, 'gi').test(item.titulo));
    } else {
      this.lista = this.listaCompleta;
    }
  }

  openBottomSheet(id: string, index: number): void {

    const itens: BottomSheetItem[] = [
      {
        codigo: 'editar',
        titulo: 'Editar',
        subTitulo: 'Abre uma nova tela para edição',
        icone: 'edit'
      },
      {
        codigo: 'excluir',
        titulo: 'Excluir',
        subTitulo: 'Exclui o tarefa',
        icone: 'clear',
        cor: '#FF0000'
      }
    ];

    this.bottomSheetService.abrir(itens, BottomSheetComponent)
      .subscribe(codigo => {
        if (codigo) {
          switch (codigo) {
            case 'editar':
              super.editar(id);
              break;
            case 'excluir':
              if (!this.testarTarefaFixo(this.lista[index])) {
                super.excluir(index);
              }
              break;
          }
        }
      });
  }

  testarTarefaFixo(tarefa: Tarefa): boolean {
    if (tarefa.titulo === constantes.nomeDefault) {
      this.snackBar.open('Este registro não pode ser alterado ou excluído.');
      return true;
    }
    return false;
  }

  atualizarDados() {
    this.buscarDadosNaApi();
  }

  private buscarDadosNaApi() {

    this.lista = [];
    this.listaCompleta = [];

    this.tarefaApiService.filtrar(this.filtros)
      .subscribe({
        next: (tarefas: Tarefa[]) => {
          this.lista = tarefas;
          this.listaCompleta = tarefas;
        },
        error: (error: HttpErrorResponse) => this.snackBar.open(error.message)
      });
  }
}
