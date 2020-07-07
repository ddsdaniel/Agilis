import { Entidade } from '../../entidade';
import { AtorFK } from '../../pessoas/ator-fk';
import { TemaFK } from '../temas/tema-fk';

export interface Produto extends Entidade {
  timeId: string;
  temas: TemaFK[];
  atores: AtorFK[];
}
