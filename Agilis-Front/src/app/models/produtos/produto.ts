import { Entidade } from '../entidade';
import { Epico } from './epico';

export interface Produto extends Entidade {
  nome: string;
  descricao: string;
  urlRepositorio: string;
  epicos: Epico[];
}
