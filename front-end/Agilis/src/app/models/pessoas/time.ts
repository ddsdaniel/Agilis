import { EscopoTime } from 'src/app/enums/pessoas/escopo-time.enum';

import { Entidade } from '../entidade';
import { Produto } from '../trabalho/produtos/produto';
import { ReleaseFK } from '../trabalho/releases/release-fk';
import { UsuarioFK } from './usuario-FK';

export interface Time extends Entidade {
  escopo: EscopoTime;
  administradores: UsuarioFK[];
  colaboradores: UsuarioFK[];
  produtos: Produto[];
  releases: ReleaseFK[];
}
