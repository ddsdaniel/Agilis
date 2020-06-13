import { Entidade } from '../../entidade';
import { TimeVO } from '../../pessoas/time-vo';
import { SprintVO } from '../sprints/sprint-vo';

export interface Release extends Entidade {
  time: TimeVO;
  ordem: number;
  sprints: SprintVO[];
}
