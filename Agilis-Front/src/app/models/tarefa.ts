import { Entidade } from './entidade';

export interface Tarefa extends Entidade {
  titulo: string;
  descricao: string;
}
