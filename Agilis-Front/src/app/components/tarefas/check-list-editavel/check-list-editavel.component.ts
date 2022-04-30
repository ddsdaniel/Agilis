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

  @Input() checkList: CheckList;

  editando = false;
  textoEdicao = '';

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
}
