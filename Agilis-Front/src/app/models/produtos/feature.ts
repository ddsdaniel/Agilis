import { Entidade } from '../entidade';
import { Produto } from './produto';

export interface Feature extends Entidade  {
  nome: string;
  produto: Produto;
}
