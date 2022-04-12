import { Entidade } from './entidade';

export interface Sprint extends Entidade {
  nome: string;
  objetivos: string;
  dataInicial?: Date;
  dataFinal?: Date;
}
