import { Entidade } from './entidade';

export interface EntidadeNodo extends Entidade {
  rota: string;
  rotaFilho: string;
  filhos?: EntidadeNodo[];
}
