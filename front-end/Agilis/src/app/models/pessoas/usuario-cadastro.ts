import { RegraUsuario } from 'src/app/enums/pessoas/regra-usuario.enum';

export interface UsuarioCadastro {
  email: string;
  nome: string;
  sobrenome: string;
  senha: string;
  confirmaSenha: string;
  regra: RegraUsuario;
}
