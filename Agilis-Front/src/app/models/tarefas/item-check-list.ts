import { Entidade } from '../entidade';
import { CheckList } from './check-list';

export interface ItemCheckList extends Entidade {
  nome: string;
  concluido: boolean;
  horasPrevistas: string;
  ordem: number;
  checkList: CheckList;
}
