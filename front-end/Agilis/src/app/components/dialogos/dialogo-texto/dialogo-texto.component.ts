import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { DialogoTexto } from 'src/app/models/dialogos/dialogo-texto';

@Component({
  selector: 'app-dialogo-texto',
  templateUrl: './dialogo-texto.component.html',
  styleUrls: ['./dialogo-texto.component.scss']
})
export class DialogoTextoComponent implements OnInit {

  constructor(
    public dialogRef: MatDialogRef<DialogoTextoComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogoTexto) {
  }

  cancelar(): void {
    this.dialogRef.close();
  }

  ngOnInit() {
  }

  salvar(){
    this.dialogRef.close(this.data.texto);
  }
}
