import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { constantes } from 'src/app/consts/constantes';
import { BottomSheetItem } from 'src/app/models/bottom-sheet-item';
import { Produto } from 'src/app/models/produtos/produto';
import { ProdutoApiService } from 'src/app/services/apis/produto-api.service';
import { BottomSheetService } from 'src/app/services/bottom-sheet.service';
import { TituloService } from 'src/app/services/titulo.service';

import { CrudListComponent } from '../../crud/crud-list-component';
import { BottomSheetComponent } from '../../widgets/bottom-sheet/bottom-sheet.component';


@Component({
  selector: 'app-produtos-list',
  templateUrl: './produtos-list.component.html',
  styleUrls: ['./produtos-list.component.scss']
})
export class ProdutosListComponent extends CrudListComponent<Produto> {

  constructor(
    produtoApiService: ProdutoApiService,
    public snackBar: MatSnackBar,
    router: Router,
    private bottomSheetService: BottomSheetService,
    tituloService: TituloService,
  ) {
    super(produtoApiService, snackBar, router, 'produtos');

    tituloService.definir('Produtos');
  }

  onPesquisar(criterio: string) {
    if (criterio) {
      this.lista = (this.listaCompleta || [])
        .filter(item => new RegExp(criterio, 'gi').test(item.nome));
    } else {
      this.lista = this.listaCompleta;
    }
  }

  openBottomSheet(id: string, index: number): void {

    const itens: BottomSheetItem[] = [
      {
        codigo: 'backlog',
        titulo: 'Backlog',
        subTitulo: 'Abre o product backlog',
        icone: 'task'
      },
      {
        codigo: 'editar',
        titulo: 'Editar',
        subTitulo: 'Abre uma nova tela para edição',
        icone: 'edit'
      },
      {
        codigo: 'excluir',
        titulo: 'Excluir',
        subTitulo: 'Exclui o produto',
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
              if (!this.testarProdutoFixo(this.lista[index])) {
                super.excluir(index);
              }
              break;
            case 'backlog':
              this.router.navigateByUrl(`produtos/${id}/backlog`);
              break;
          }
        }
      });
  }

  testarProdutoFixo(produto: Produto): boolean {
    if (produto.nome === constantes.nomeDefault) {
      this.snackBar.open('Este registro não pode ser alterado ou excluído.');
      return true;
    }
    return false;
  }
}
