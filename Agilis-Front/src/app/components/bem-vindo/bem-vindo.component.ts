import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Recurso } from 'src/app/models/recurso';
import { TituloService } from 'src/app/services/titulo.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-bem-vindo',
  templateUrl: './bem-vindo.component.html',
  styleUrls: ['./bem-vindo.component.scss']
})
export class BemVindoComponent implements OnInit {

  recursos: Recurso[] = [
    {
      titulo: 'Transações',
      descricao: 'Os caderninhos e as planilhas ficaram no passado. Modernize o controle das suas finanças de um jeito fácil e moderno.',
      imagemUrl: '../../../assets/images/bem-vindo/transacoes.png',
      imagemAlt: 'Tela de transações',
    },
    {
      titulo: 'Filtros Avançados',
      descricao: 'Acesse o seu histórico e localize rapidamente qualquer registro com essa incrível combinação de filtros.',
      imagemUrl: '../../../assets/images/bem-vindo/filtros-avancados.png',
      imagemAlt: 'Filtros avançados',
    },
    {
      titulo: 'Único ponto de partida',
      descricao: 'Com esse menu central fica muito mais fácil fazer qualquer coisa no Agilis. Tudo em um só lugar.',
      imagemUrl: '../../../assets/images/bem-vindo/menu.png',
      imagemAlt: 'Menu principal',
    },
    {
      titulo: 'Dinheiro Acumulado',
      descricao: 'Ter controle sobre o seu dinheiro e consciência de cada ação realizada sobre ele é a ' +
        'essência da educação financeira. Que tal uma previsão automática do seu saldo acumulado nos próximos seis meses?',
      imagemUrl: '../../../assets/images/bem-vindo/dinheiro-acumulado.png',
      imagemAlt: 'Dinheiro Acumulado',
    },
    {
      titulo: 'Despesas por Categorias',
      descricao: 'Você já se perguntou onde foi o meu salário? ' +
        'Aqui você descobre quais são as categorias mais representativas tanto em valor quanto em percentual. ' +
        'Reflita sobre as suas despesas e faça ajustes para corrigir o curso.',
      imagemUrl: '../../../assets/images/bem-vindo/despesas-por-categorias.png',
      imagemAlt: 'Despesas por Categorias',
    },
    {
      titulo: 'Faturas do Cartão de Crédito',
      descricao: 'Você sabia que o cartão de crédito representa em torno de 80% das dívidas ' +
        'das famílias brasileiras. Provavelmente por que você vai comprando e não vê o dinheiro ' +
        'saindo, por consequência, não tem controle sobre o total da sua fatura atual. ' +
        'Registrando tudo no Agilis, você visualiza e detalha todas as suas faturas, inclusive as futuras, a qualquer momento.',
      imagemUrl: '../../../assets/images/bem-vindo/faturas.png',
      imagemAlt: 'Faturas',
    },
  ];

  constructor(
    private tituloService: TituloService,
    private router: Router,
  ) { }

  ngOnInit() {
    this.tituloService.definir('Bem-vindo');
  }

}
