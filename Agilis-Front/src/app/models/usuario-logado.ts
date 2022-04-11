import { UsuarioConsulta } from './usuario-consulta';

export interface UsuarioLogado {
  usuario: UsuarioConsulta;
  token: string;
  tipoToken: string;
  refreshToken: string;
}
