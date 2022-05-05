import { Entidade } from '../entidade';
import { ItemCheckList } from './item-check-list';
import { Tarefa } from './tarefa';

export interface CheckList extends Entidade {
  nome: string;
  itens: ItemCheckList[];
  ordem: number;
  tarefa: Tarefa;
}
