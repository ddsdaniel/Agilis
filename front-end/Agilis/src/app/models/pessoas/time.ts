import { EscopoTime } from 'src/app/enums/pessoas/escopo-time.enum';

import { Entidade } from '../entidade';
import { Produto } from '../trabalho/produto';
import { ReleaseFK } from '../trabalho/releases/release-fk';
import { UsuarioVO } from './usuario-vo';

export interface Time extends Entidade {
  escopo: EscopoTime;
  administradores: UsuarioVO[];
  colaboradores: UsuarioVO[];
  produtos: Produto[];
  releases: ReleaseFK[];
}
