import { SituacaoTarefa } from 'src/app/enums/situacao-tarefa.enum';
import { TipoTarefa } from '../../enums/tipo-tarefa.enum';
import { AnexoFk } from '../anexo-fk';
import { Cliente } from '../cliente';
import { Entidade } from '../entidade';
import { Feature } from '../produtos/feature';
import { UsuarioConsulta } from '../seguranca/usuario-consulta';
import { Sprint } from '../sprint';
import { CheckList } from './check-list';
import { Comentario } from './comentario';

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
  urlTicketSAC: string;
  comentarios: Comentario[];
  anexos: AnexoFk[];
  sprint: Sprint;
  situacao: SituacaoTarefa;
  branches: string;
  solucao: string;
}
