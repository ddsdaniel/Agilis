import { ForeignKey } from '../foreign-key';
import { Email } from './email';

export interface UsuarioFK extends ForeignKey {
  email: Email;
}
