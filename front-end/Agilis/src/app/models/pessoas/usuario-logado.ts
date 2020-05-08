import { Usuario } from './usuario';

export interface UsuarioLogado extends Usuario {
  token: string;
  tipoToken: string;
}
