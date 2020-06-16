import { Entidade } from '../../entidade';
import { SprintFK } from '../sprints/sprint-fk';

export interface Release extends Entidade {
  sprints: SprintFK[];
}
