import { Entidade } from '../entidade';

export interface Tag extends Entidade {
  nome: string;
  cor: string;
}
