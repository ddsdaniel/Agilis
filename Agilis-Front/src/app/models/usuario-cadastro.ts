import { RegraUsuario } from '../enums/regra-usuario.enum';
import { Entidade } from './entidade';

export interface UsuarioCadastro extends Entidade {
  nome: string;
  sobrenome: string;
  senha: string;
  confirmaSenha: string;
  email: string;
  ativo: boolean;
  regra: RegraUsuario;
}
