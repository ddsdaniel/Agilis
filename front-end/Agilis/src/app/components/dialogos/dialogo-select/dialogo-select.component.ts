import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DialogoSelect } from 'src/app/models/dialogos/dialogo-select';
import { ItemSelect } from 'src/app/models/item-select';

@Component({
  selector: 'app-dialogo-select',
  templateUrl: './dialogo-select.component.html',
  styleUrls: ['./dialogo-select.component.scss']
})
export class DialogoSelectComponent implements OnInit {

  constructor(
    public dialogRef: MatDialogRef<DialogoSelectComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogoSelect) {
  }

  cancelar(): void {
    this.dialogRef.close();
  }

  ngOnInit() {
  }

  salvar() {
    const item: ItemSelect = this.data.lista.find(i => i.id === this.data.idAtual);
    this.dialogRef.close(item);
  }
}
