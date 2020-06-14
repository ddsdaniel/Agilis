import { EscopoTime } from 'src/app/enums/pessoas/escopo-time.enum';

import { Entidade } from '../entidade';
import { Produto } from '../trabalho/produto';
import { ReleaseVO } from '../trabalho/releases/release-vo';
import { UsuarioVO } from './usuario-vo';

export interface Time extends Entidade {
  escopo: EscopoTime;
  administradores: UsuarioVO[];
  colaboradores: UsuarioVO[];
  produtos: Produto[];
  releases: ReleaseVO[];
}
