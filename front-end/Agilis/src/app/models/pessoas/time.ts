import { EscopoTime } from 'src/app/enums/pessoas/escopo-time.enum';

import { Entidade } from '../entidade';
import { UsuarioFK } from './usuario-FK';

export interface Time extends Entidade {
  escopo: EscopoTime;
  administradores: UsuarioFK[];
  colaboradores: UsuarioFK[];
}
