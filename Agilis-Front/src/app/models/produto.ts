import { Entidade } from './entidade';
import { Tarefa } from './tarefa';

export interface Produto extends Entidade {
  nome: string;
  descricao: string;
  urlRepositorio: string;
  backlog?: Tarefa[];
}
