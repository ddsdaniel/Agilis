import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-year-navigation',
  templateUrl: './year-navigation.component.html',
  styleUrls: ['./year-navigation.component.scss']
})
export class YearNavigationComponent implements OnInit {

  @Input() ano = 2021;
  @Output() navegou = new EventEmitter();

  constructor() {
    this.ano = new Date().getFullYear();
  }

  ngOnInit(): void {
  }

  ir() {
    this.ano++;
    this.notificar();
  }

  voltar() {
    this.ano--;
    this.notificar();
  }

  private notificar() {

    const params = {
      ano: this.ano
    };

    this.navegou.emit(params);
  }

}
