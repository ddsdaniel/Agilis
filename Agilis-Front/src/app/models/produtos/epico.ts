import { Entidade } from '../entidade';
import { Feature } from './feature';
import { Produto } from './produto';

export interface Epico extends Entidade {
  nome: string;
  produto: Produto;
  features: Feature[];
}
