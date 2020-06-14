import { BasicVO } from '../basic-vo';
import { Email } from './email';

export interface UsuarioVO extends BasicVO {
  email: Email;
}
