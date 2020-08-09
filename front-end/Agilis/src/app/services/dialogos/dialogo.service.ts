import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { DialogoEmailComponent } from 'src/app/components/dialogos/dialogo-email/dialogo-email.component';
import { DialogoSelectComponent } from 'src/app/components/dialogos/dialogo-select/dialogo-select.component';
import { DialogoTextoComponent } from 'src/app/components/dialogos/dialogo-texto/dialogo-texto.component';
import { DialogoSelect } from 'src/app/models/dialogos/dialogo-select';
import { DialogoTexto } from 'src/app/models/dialogos/dialogo-texto';
import { Email } from 'src/app/models/pessoas/email';
import { ItemSelect } from 'src/app/models/item-select';

@Injectable({
  providedIn: 'root'
})
export class DialogoService {

  constructor(
    private dialog: MatDialog,
  ) { }

  abrirEmail(): Observable<Email> {
    const dialogRef = this.dialog.open(DialogoEmailComponent, {
      data: {
        endereco: ''
      },
      width: '400px'
    });

    return dialogRef.afterClosed().pipe(take(1));
  }

  abrirTexto(titulo: string, label: string, valorDefault: string = ''): Observable<string> {

    const data: DialogoTexto = {
      titulo,
      label,
      texto: valorDefault
    };

    const config = {
      data,
      width: '400px'
    };

    const dialogRef = this.dialog.open(DialogoTextoComponent, config);

    return dialogRef.afterClosed().pipe(take(1));
  }

  abrirSelect(lista: ItemSelect[], titulo: string, label: string, defaultId: number = 0)
    : Observable<ItemSelect> {

    const data: DialogoSelect = {
      titulo,
      label,
      idAtual: defaultId,
      lista
    };

    const config = {
      data,
      width: '400px'
    };

    const dialogRef = this.dialog.open(DialogoSelectComponent, config);

    return dialogRef.afterClosed().pipe(take(1));
  }
}
