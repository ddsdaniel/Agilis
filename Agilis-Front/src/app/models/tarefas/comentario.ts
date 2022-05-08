import { UsuarioConsulta } from '../seguranca/usuario-consulta';

export interface Comentario {
  descricao: string;
  autor: UsuarioConsulta;
  dataHora: Date;
}
