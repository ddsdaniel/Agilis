import { TipoTarefa } from '../../enums/tipo-tarefa.enum';
import { Entidade } from '../entidade';
import { Feature } from '../produtos/feature';
import { UsuarioConsulta } from '../usuario-consulta';

export interface Tarefa extends Entidade {
  titulo: string;
  descricao: string;
  featureId: string;
  feature?: Feature;
  tipo: TipoTarefa;
  dev?: UsuarioConsulta;
  devId?: string;
  tester?: UsuarioConsulta;
  testerId?: string;
  analista?: UsuarioConsulta;
  analistaId?: string;
}
