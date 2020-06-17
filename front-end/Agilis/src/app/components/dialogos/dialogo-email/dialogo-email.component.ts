import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Email } from 'src/app/models/pessoas/email';

@Component({
  selector: 'app-dialogo-email',
  templateUrl: './dialogo-email.component.html',
  styleUrls: ['./dialogo-email.component.scss']
})
export class DialogoEmailComponent implements OnInit {

  constructor(
    public dialogRef: MatDialogRef<DialogoEmailComponent>,
    @Inject(MAT_DIALOG_DATA) public email: Email) {
  }

  cancelar(): void {
    this.dialogRef.close();
  }

  ngOnInit() {
  }

}
