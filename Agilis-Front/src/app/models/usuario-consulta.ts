import { RegraUsuario } from '../enums/regra-usuario.enum';
import { Entidade } from './entidade';

export interface UsuarioConsulta extends Entidade {
  nome: string;
  sobrenome: string;
  email: string;
  ativo: boolean;
  licencaCompleta: boolean;
  regra: RegraUsuario;
}
