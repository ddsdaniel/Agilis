import { Entidade } from '../../entidade';
import { Time } from '../../pessoas/time';

export interface Release extends Entidade {
  time: Time;
}
