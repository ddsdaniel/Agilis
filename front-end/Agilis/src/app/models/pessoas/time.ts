import { Entidade } from '../entidade';
import { EscopoTime } from 'src/app/enums/pessoas/escopo-time.enum';
import { UsuarioVO } from './usuario-vo';
import { ProdutoVO } from '../trabalho/produtos/produto-vo';

export interface Time extends Entidade {
  escopo: EscopoTime;
  administradores: UsuarioVO[];
  colaboradores: UsuarioVO[];
  produtos: ProdutoVO[];
}
