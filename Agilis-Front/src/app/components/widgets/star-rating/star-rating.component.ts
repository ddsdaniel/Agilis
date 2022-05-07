import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-star-rating',
  templateUrl: './star-rating.component.html',
  styleUrls: ['./star-rating.component.scss'],

})
export class StarRatingComponent {

  @Input() label = '';
  @Input() maximo = 5;
  @Input() valor = 0;
  @Output() valorChange = new EventEmitter<number>();

  counter(i: number) {
    return new Array(i);
  }

  marcar(i: number) {
    if ((i + 1) === this.valor) {
      this.valor = 0;
    } else {
      this.valor = i + 1;
    }
    this.valorChange.emit(this.valor);
  }

}
