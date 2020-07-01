import { TarefaFK } from './tarefas/tarefa-fk';

export interface Fase {
  posicao: number;
  nome: string;
  tarefas: TarefaFK[];
}
