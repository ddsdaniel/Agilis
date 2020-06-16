import { ForeignKey } from '../foreign-key';
import { Email } from './email';

export interface UsuarioVO extends ForeignKey {
  email: Email;
}
