import { Entidade } from '../../entidade';
import { IntervaloDatas } from '../../intervalo-datas';
import { ReleaseVO } from '../releases/release-vo';

export interface Sprint extends Entidade {
  release: ReleaseVO;
  numero: number;
  periodo: IntervaloDatas;
}
