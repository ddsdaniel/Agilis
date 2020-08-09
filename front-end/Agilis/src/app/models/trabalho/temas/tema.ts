import { Epico } from '../epicos/epico';

export interface Tema {
  id: string;
  nome: string;
  epicos: Epico[];
}
