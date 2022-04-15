import { TipoTarefa } from '../enums/tipo-tarefa.enum';
import { Entidade } from './entidade';
import { Produto } from './produto';

export interface Tarefa extends Entidade {
  titulo: string;
  descricao: string;
  produtoId: string;
  produto?: Produto;
  tipo: TipoTarefa;
}
