import { Entidade } from '../../entidade';
import { EpicoFK } from '../epicos/epico-fk';

export interface Tema extends Entidade {
  produtoId: string;
  epicos: EpicoFK[];
}
