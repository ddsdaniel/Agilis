import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { constantes } from 'src/app/consts/constantes';
import { BottomSheetItem } from 'src/app/models/bottom-sheet-item';
import { Produto } from 'src/app/models/produtos/produto';
import { Tarefa } from 'src/app/models/tarefas/tarefa';
import { ProdutoApiService } from 'src/app/services/apis/produto-api.service';
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
export class TarefasListComponent extends CrudListComponent<Tarefa> {

  produto: Produto;

  constructor(
    tarefaApiService: TarefaApiService,
    router: Router,
    tituloService: TituloService,
    private bottomSheetService: BottomSheetService,
    private activatedRoute: ActivatedRoute,
    private produtoApiService: ProdutoApiService,
    public snackBar: MatSnackBar,
  ) {
    super(tarefaApiService, snackBar, router, 'tarefas');
    tituloService.definir('Product Backlog');
  }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(
      params => {

        const id = this.activatedRoute.snapshot.paramMap.get('productId');

        this.produtoApiService.obterUm(id)
          .subscribe({
            next: produto => this.produto = produto
          });
      });

    super.ngOnInit();
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
}
