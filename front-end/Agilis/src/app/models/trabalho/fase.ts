import { Tarefa } from './tarefa';

export interface Fase {
  posicao: number;
  nome: string;
  tarefas: Tarefa[];
}
