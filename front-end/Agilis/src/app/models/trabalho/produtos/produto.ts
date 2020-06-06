import { Entidade } from '../../entidade';
import { TimeVO } from '../../pessoas/time-vo';

export interface Produto extends Entidade {
  time: TimeVO;
}
