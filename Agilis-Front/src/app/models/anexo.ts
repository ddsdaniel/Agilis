import { TipoAnexo } from '../enums/tipo-anexo.enum';
import { Entidade } from './entidade';

export interface Anexo extends Entidade {
  conteudo: string;
  nome: string;
  tipo: TipoAnexo;
}
