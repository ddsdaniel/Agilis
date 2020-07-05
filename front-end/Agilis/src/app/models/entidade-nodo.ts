import { Entidade } from './entidade';

export interface EntidadeNodo extends Entidade {
  filhos?: EntidadeNodo[];
}
