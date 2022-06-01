import { Produto } from '../produtos/produto';

export interface ImportacaoTrello {
  boardId: string;
  produto: Produto;
  limparDados: boolean;
}

