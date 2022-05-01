import { Component, Input, OnInit } from '@angular/core';
import { MatSelectionListChange } from '@angular/material/list/selection-list';
import { constantes } from 'src/app/consts/constantes';
import { CheckList } from 'src/app/models/tarefas/check-list';
import { ItemCheckList } from 'src/app/models/tarefas/item-check-list';

@Component({
  selector: 'app-check-list-editavel',
  templateUrl: './check-list-editavel.component.html',
  styleUrls: ['./check-list-editavel.component.scss']
})
export class CheckListEditavelComponent implements OnInit {

  // exemplo de binding
  // https://stackblitz.com/edit/material-selection-list-5-0-0?file=app%2Fapp.component.ts

  @Input() checkList: CheckList;

  editando = false;
  textoEdicao = '';
  selectedOptions: string[] = [];

  constructor() { }

  ngOnInit(): void {
  }

  adicionarItem() {
    const item: ItemCheckList = {
      id: (this.checkList.itens.length + 1).toString(),// constantes.newGuid,
      nome: 'Novo item',
      checkList: this.checkList,
      concluido: false,
      horasPrevistas: '00:00',
      ordem: 0
    };
    this.checkList.itens.push(item);
  }

  editar() {
    let texto = this.checkList.nome + '\n';

    this.checkList.itens.forEach(item => texto += this.obterItemEdicao(item));

    this.textoEdicao = texto;

    this.editando = true;
  }

  private obterItemEdicao(item: ItemCheckList) {
    const marcado = item.concluido ? '(marcado) ' : '';
    return `- ${marcado}${item.nome}\n`;
  }

  selectionChange(event: MatSelectionListChange) {
    const item: ItemCheckList = event.options[0].value;
    item.concluido = !item.concluido;
  }

  salvar() {
    const linhas = this.textoEdicao.split('\n');

    this.checkList.nome = linhas[0];

    this.checkList.itens = [];
    this.selectedOptions = [];

    for (let i = 1; i < linhas.length; i++) {
      let linha = linhas[i];

      if (linha.startsWith('- ')) {
        linha = linha.substring(2);
      }

      let concluido = false;

      if (linha.startsWith('(marcado) ')) {
        linha = linha.substring(10);
        concluido = true;
      }

      if (linha) {

        const item: ItemCheckList = {
          id: (this.checkList.itens.length + 1).toString(),// constantes.newGuid,
          nome: linha,
          checkList: this.checkList,
          concluido,
          horasPrevistas: '00:00',
          ordem: 0
        };

        this.checkList.itens.push(item);

        if (item.concluido) {
          this.selectedOptions.push(item.id);
        }
      }
    }

    this.editando = false;

  }

  cancelar() {
    this.editando = false;
  }
}
