import { Entidade } from './entidade';

export interface Dispositivo extends Entidade {
  token: string;
}
