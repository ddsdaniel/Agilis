import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DialogoSimNaoDados } from 'src/app/models/dialogo-sim-nao-dados';

@Component({
  selector: 'app-dialogo-sim-nao',
  templateUrl: './dialogo-sim-nao.component.html',
  styleUrls: ['./dialogo-sim-nao.component.scss']
})
export class DialogoSimNaoComponent implements OnInit {

  constructor(
    public dialogRef: MatDialogRef<DialogoSimNaoComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogoSimNaoDados
  ) { }

  ngOnInit(): void {
  }

  cancelar(): void {
    this.dialogRef.close();
  }

  confirmar(): void {
    this.dialogRef.close(true);
  }

}
