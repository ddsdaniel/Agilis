import { Entidade } from '../../entidade';
import { Jornada } from './jornada';

export interface Produto extends Entidade {
  jornadas: Jornada[];
}
