import { Fase } from '../fase';

export interface Jornada {
  posicao: number;
  nome: string;
  fases: Fase[];
}
