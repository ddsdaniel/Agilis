import { EntidadeFavorita } from '../../entidade-favorita';
import { Time } from '../../pessoas/time';

export interface Produto extends EntidadeFavorita {
  time: Time;
}
