import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { constantes } from 'src/app/consts/constantes';
import { BottomSheetItem } from 'src/app/models/bottom-sheet-item';
import { Time } from 'src/app/models/time';
import { TimeApiService } from 'src/app/services/apis/time-api.service';
import { BottomSheetService } from 'src/app/services/bottom-sheet.service';
import { TituloService } from 'src/app/services/titulo.service';

import { CrudListComponent } from '../../crud/crud-list-component';
import { BottomSheetComponent } from '../../widgets/bottom-sheet/bottom-sheet.component';


@Component({
  selector: 'app-times-list',
  templateUrl: './times-list.component.html',
  styleUrls: ['./times-list.component.scss']
})
export class TimesListComponent extends CrudListComponent<Time> {

  constructor(
    timeApiService: TimeApiService,
    public snackBar: MatSnackBar,
    router: Router,
    private bottomSheetService: BottomSheetService,
    tituloService: TituloService,
  ) {
    super(timeApiService, snackBar, router, 'times');

    tituloService.definir('Times');
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
        codigo: 'editar',
        titulo: 'Editar',
        subTitulo: 'Abre uma nova tela para edição',
        icone: 'edit'
      },
      {
        codigo: 'excluir',
        titulo: 'Excluir',
        subTitulo: 'Exclui o time',
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
              if (!this.testarTimeFixo(this.lista[index])) {
                super.excluir(index);
              }
              break;
          }
        }
      });
  }

  testarTimeFixo(time: Time): boolean {
    if (time.nome === constantes.nomeDefault) {
      this.snackBar.open('Este registro não pode ser alterado ou excluído.');
      return true;
    }
    return false;
  }
}
