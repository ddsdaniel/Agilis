import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Intro } from 'src/app/models/intro';

@Component({
  selector: 'app-intro',
  templateUrl: './intro.component.html',
  styleUrls: ['./intro.component.scss']
})
export class IntroComponent implements OnInit {

  indiceAtual = 0;

  itens: Intro[] = [
    {
      titulo: 'Menu',
      explicacao: 'Aqui você tem acesso a todos os recursos do sistema.',
      imagem: '../../../assets/images/intro/menu.png'
    },
  ];

  constructor(
    private router: Router,
  ) { }

  ngOnInit(): void {

  }

  onAvancar() {
    if (this.indiceAtual === this.itens.length - 1) {
      this.router.navigate(['/transacoes']);
    } else {
      this.indiceAtual++;
    }
  }

  onVoltar() {
    this.indiceAtual--;
  }

  obterTextoAvancar(): string {
    if (this.indiceAtual === this.itens.length - 1) {
      return 'ENTENDI';
    } else {
      return 'AVANÇAR';
    }
  }
}
