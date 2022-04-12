import { Entidade } from './entidade';

export interface Produto extends Entidade {
  nome: string;
  descricao: string;
  urlRepositorio: string;
}
