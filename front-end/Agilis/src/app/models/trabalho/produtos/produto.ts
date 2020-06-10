import { Entidade } from '../../entidade';
import { TimeVO } from '../../pessoas/time-vo';
import { SprintVO } from '../sprints/sprint-vo';

export interface Produto extends Entidade {
  time: TimeVO;
  sprints: SprintVO[];
}
