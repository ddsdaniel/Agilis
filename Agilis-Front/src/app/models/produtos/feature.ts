import { Entidade } from '../entidade';
import { Tarefa } from '../tarefas/tarefa';
import { Epico } from './epico';

export interface Feature extends Entidade {
  nome: string;
  epicoId: string;
  epico?: Epico;
  tarefas: Tarefa[];
}
