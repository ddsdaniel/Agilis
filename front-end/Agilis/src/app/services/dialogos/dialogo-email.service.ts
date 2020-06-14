import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { DialogoEmailComponent } from 'src/app/components/dialogo-email/dialogo-email.component';
import { Email } from 'src/app/models/pessoas/email';

@Injectable({
  providedIn: 'root'
})
export class DialogoEmailService {

  constructor(
    private dialog: MatDialog,
  ) { }

  abrir(): Observable<Email> {
    const dialogRef = this.dialog.open(DialogoEmailComponent, {
      data: {
        endereco: ''
      },
      width: '400px'
    });

    return dialogRef.afterClosed().pipe(take(1));
  }
}
