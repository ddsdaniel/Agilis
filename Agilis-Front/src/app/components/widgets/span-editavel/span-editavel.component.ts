import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-span-editavel',
  templateUrl: './span-editavel.component.html',
  styleUrls: ['./span-editavel.component.scss']
})
export class SpanEditavelComponent {

  @Input() editavel: true;
  @Input() label = '';
  @Input() value = '';
  @Output() valueChange = new EventEmitter<string>();

  private backupValue = '';

  editando = false;

  editar() {
    if (this.editavel) {
      this.backupValue = this.value;
      this.editando = true;
    }
  }

  cancelar() {
    this.value = this.backupValue;
    this.editando = false;
  }

  aplicar() {
    this.editando = false;
    this.valueChange.emit(this.value);
  }

}
