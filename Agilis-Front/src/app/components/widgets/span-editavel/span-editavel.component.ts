import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-span-editavel',
  templateUrl: './span-editavel.component.html',
  styleUrls: ['./span-editavel.component.scss']
})
export class SpanEditavelComponent implements OnInit {

  @Input() value = '';
  @Input() label = '';
  @Input() checkBox = false;
  @Input() marcado = false;
  @Output() removeEvent = new EventEmitter();

  private backupValue = '';

  editando = false;

  constructor() { }

  ngOnInit(): void {
  }

  editar() {
    this.backupValue = this.value;
    this.editando = true;
  }

  cancelar() {
    this.value = this.backupValue;
    this.editando = false;
  }

  aplicar() {
    this.editando = false;
  }

  excluir() {
    this.removeEvent.emit();
  }
}
