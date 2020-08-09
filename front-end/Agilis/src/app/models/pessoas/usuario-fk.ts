import { ForeignKey } from '../foreign-key';

export interface UsuarioFK extends ForeignKey {
  email: string;
}
