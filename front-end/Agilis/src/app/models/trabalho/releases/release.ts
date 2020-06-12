import { Entidade } from '../../entidade';
import { TimeVO } from '../../pessoas/time-vo';

export interface Release extends Entidade {
  time: TimeVO;
  ordem: number;
}
