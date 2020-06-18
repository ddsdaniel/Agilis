import { Fase } from './fase';
import { PrioridadeProductBacklog } from 'src/app/enums/trabalho/prioridade-product-backlog.enum';
import { ItemProductBacklog } from './item-product-backlog';

export interface ProductBacklog {
  fases: Fase[];
  prioridades: PrioridadeProductBacklog[];
  itens: ItemProductBacklog[];
}
