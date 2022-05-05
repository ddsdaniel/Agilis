import { Component, Input, OnInit } from '@angular/core';
import { constantes } from 'src/app/consts/constantes';
import { CheckList } from 'src/app/models/tarefas/check-list';
import { ItemCheckList } from 'src/app/models/tarefas/item-check-list';

@Component({
  selector: 'app-check-list-editavel',
  templateUrl: './check-list-editavel.component.html',
  styleUrls: ['./check-list-editavel.component.scss']
})
export class CheckListEditavelComponent implements OnInit {

  @Input() checkList: CheckList;

  editando = false;
  textoEdicao = '';

  constructor() { }

  ngOnInit(): void {
  }

  adicionarItem() {
    const ordem = this.checkList.itens && this.checkList.itens.length > 0
      ? Math.max.apply(null, this.checkList.itens.map(x => x.ordem)) + 1
      : 1;

    const item: ItemCheckList = {
      id: constantes.newGuid,
      nome: 'Novo item',
      checkList: null,
      concluido: false,
      horasPrevistas: '00:00',
      ordem
    };
    this.checkList.itens.push(item);
  }

  editar() {
    let texto = this.checkList.nome + '\n';

    this.checkList.itens.forEach(item => texto += this.obterItemEdicao(item));

    // remove o último enter
    if (texto.endsWith('\n')) {
      texto = texto.substring(0, texto.length - 1);
    }

    this.textoEdicao = texto;

    this.editando = true;
  }

  private obterItemEdicao(item: ItemCheckList) {
    const bullet = item.concluido ? '*' : '-';
    return `${bullet} ${item.nome}\n`;
  }

  salvar() {
    const linhas = this.textoEdicao.split('\n');

    this.checkList.nome = linhas[0];

    this.checkList.itens = [];

    for (let i = 1; i < linhas.length; i++) {
      let linha = linhas[i];

      const concluido = linha.startsWith('*');

      if (linha.startsWith('- ') || linha.startsWith('* ')) {
        linha = linha.substring(2);
      }

      if (linha) {

        const item: ItemCheckList = {
          id: constantes.newGuid,
          nome: linha,
          checkList: null,
          concluido,
          horasPrevistas: '00:00',
          ordem: i
        };

        this.checkList.itens.push(item);

      }
    }

    this.editando = false;

  }

  cancelar() {
    this.editando = false;
  }
}
