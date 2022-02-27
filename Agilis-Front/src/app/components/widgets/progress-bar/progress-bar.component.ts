import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-progress-bar',
  templateUrl: './progress-bar.component.html',
  styleUrls: ['./progress-bar.component.scss']
})
export class ProgressBarComponent implements OnInit {

  @Input() valor = 10;
  @Input() maximo = 100;

  constructor() { }

  ngOnInit(): void {
  }

  percentual(): number {
    return Math.trunc(this.valor / this.maximo * 100);
  }
}
