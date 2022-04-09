import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DialogoInput } from 'src/app/models/dialogo-input';

@Component({
  selector: 'app-dialogo-input',
  templateUrl: './dialogo-input.component.html',
  styleUrls: ['./dialogo-input.component.scss']
})
export class DialogoInputComponent implements OnInit {

  resposta = '';

  constructor(
    public dialogRef: MatDialogRef<DialogoInputComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogoInput
  ) { }

  ngOnInit(): void {
    if (this.data.valorInicial) {
      this.resposta = this.data.valorInicial;
    }
  }

  cancelar(): void {
    this.dialogRef.close();
  }

  confirmar(): void {
    this.dialogRef.close(this.resposta);
  }

}
