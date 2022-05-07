import { TipoTarefa } from '../../enums/tipo-tarefa.enum';
import { Cliente } from '../cliente';
import { Entidade } from '../entidade';
import { Feature } from '../produtos/feature';
import { UsuarioConsulta } from '../seguranca/usuario-consulta';
import { CheckList } from './check-list';

export interface Tarefa extends Entidade {
  titulo: string;
  descricao: string;
  feature: Feature;
  tipo: TipoTarefa;
  relator: UsuarioConsulta;
  solucionador?: UsuarioConsulta;
  horasPrevistas: string;
  horasRealizadas: string;
  tags: string[];
  checkLists: CheckList[];
  cliente: Cliente;
  valor: number;
}
