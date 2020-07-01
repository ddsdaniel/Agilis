import { Entidade } from '../../entidade';
import { SprintFK } from '../sprints/sprint-fk';
import { ProductBacklog } from '../product-backlog';

export interface Release extends Entidade {
  sprints: SprintFK[];
  productBacklog: ProductBacklog;
}
