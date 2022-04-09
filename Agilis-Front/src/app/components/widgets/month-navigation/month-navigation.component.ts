import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-month-navigation',
  templateUrl: './month-navigation.component.html',
  styleUrls: ['./month-navigation.component.scss']
})
export class MonthNavigationComponent implements OnInit {

  @Input() mes = 10;
  @Input() ano = 2021;
  meses: string[] = ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'];

  @Output() navegou = new EventEmitter();

  constructor() {
    this.mes = new Date().getMonth() + 1;
    this.ano = new Date().getFullYear();
  }

  ngOnInit(): void {
  }

  ir() {
    if (this.mes === 12) {
      this.mes = 1;
      this.ano++;
    }
    else {
      this.mes++;
    }
    this.notificar();
  }

  voltar() {
    if (this.mes === 1) {
      this.mes = 12;
      this.ano--;
    }
    else {
      this.mes--;
    }
    this.notificar();
  }

  private notificar() {
    const params = {
      dataMes: new Date(this.ano, this.mes - 1, 1),
      mes: this.mes,
      ano: this.ano
    };

    this.navegou.emit(params);
  }

  nomeMes() {
    return this.meses[this.mes - 1];
  }
}
