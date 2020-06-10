import { Entidade } from '../../entidade';
import { TimeVO } from '../../pessoas/time-vo';

export interface Sprint extends Entidade {
  time: TimeVO;
}
