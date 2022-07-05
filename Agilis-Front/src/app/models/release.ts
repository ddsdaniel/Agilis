import { Entidade } from './entidade';
import { Produto } from './produtos/produto';

export interface Release extends Entidade {
  nome: string;
  observacoes: string;
  produto: Produto,
  versao: string;
}
