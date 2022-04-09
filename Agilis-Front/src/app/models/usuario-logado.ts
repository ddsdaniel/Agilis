import { Dispositivo } from './dispositivo';
import { UsuarioConsulta } from './usuario-consulta';

export interface UsuarioLogado {
  usuario: UsuarioConsulta;
  token: string;
  tipoToken: string;
  dispositivo: Dispositivo;
  refreshToken: string;
}
