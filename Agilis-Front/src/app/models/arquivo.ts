import { Entidade } from './entidade';

export interface Arquivo extends Entidade {
  base64: string;
  nome: string;
}
