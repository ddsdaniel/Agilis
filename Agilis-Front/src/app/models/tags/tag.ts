import { Entidade } from '../entidade';
import { Tarefa } from '../tarefas/tarefa';

export interface Tag extends Entidade {
  nome: string;
  cor: string;
  tarefas: Tarefa[];
}
