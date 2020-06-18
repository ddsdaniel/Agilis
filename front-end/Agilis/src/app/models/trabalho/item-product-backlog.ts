import { Tarefa } from './tarefa';
import { PrioridadeProductBacklog } from 'src/app/enums/trabalho/prioridade-product-backlog.enum';
import { Fase } from './fase';

export interface ItemProductBacklog {
  posicao: number;
  tarefa: Tarefa;
  prioridade: PrioridadeProductBacklog;
  fase: Fase;
}
