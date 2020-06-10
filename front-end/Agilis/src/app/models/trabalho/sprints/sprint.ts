import { Entidade } from '../../entidade';
import { IntervaloDatas } from '../../intervalo-datas';
import { ProdutoVO } from '../produtos/produto-vo';

export interface Sprint extends Entidade {
  produto: ProdutoVO;
  numero: number;
  periodo: IntervaloDatas;
}
