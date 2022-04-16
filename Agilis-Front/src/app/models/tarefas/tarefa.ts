import { TipoTarefa } from '../../enums/tipo-tarefa.enum';
import { Entidade } from '../entidade';
import { Feature } from '../produtos/feature';

export interface Tarefa extends Entidade {
  titulo: string;
  descricao: string;
  featureId: string;
  feature?: Feature;
  tipo: TipoTarefa;
}
