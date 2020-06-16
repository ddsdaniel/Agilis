import { Entidade } from '../../entidade';
import { IntervaloDatas } from '../../intervalo-datas';

export interface Sprint extends Entidade {
  periodo: IntervaloDatas;
}
