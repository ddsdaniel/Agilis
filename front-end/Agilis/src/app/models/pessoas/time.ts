import { EscopoTime } from 'src/app/enums/pessoas/escopo-time.enum';

import { Entidade } from '../entidade';
import { ReleaseFK } from '../trabalho/releases/release-fk';
import { UsuarioFK } from './usuario-FK';
import { ProdutoFK } from '../trabalho/produtos/produto-fk';

export interface Time extends Entidade {
  escopo: EscopoTime;
  administradores: UsuarioFK[];
  colaboradores: UsuarioFK[];
  produtos: ProdutoFK[];
  releases: ReleaseFK[];
}
