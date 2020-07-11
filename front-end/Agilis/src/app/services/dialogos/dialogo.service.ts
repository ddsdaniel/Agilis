import { Injectable } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { DialogoEmailComponent } from 'src/app/components/dialogos/dialogo-email/dialogo-email.component';
import { DialogoTextoComponent } from 'src/app/components/dialogos/dialogo-texto/dialogo-texto.component';
import { Email } from 'src/app/models/pessoas/email';
import { DialogoTexto } from 'src/app/models/dialogos/dialogo-texto';

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
}
