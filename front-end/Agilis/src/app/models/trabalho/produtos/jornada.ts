import { Fase } from '../fase';

export interface Jornada {
  id: string;
  nome: string;
  fases: Fase[];
}
