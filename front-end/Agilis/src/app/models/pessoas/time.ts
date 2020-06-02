import { Entidade } from '../entidade';
import { EscopoTime } from 'src/app/enums/pessoas/escopo-time.enum';

export interface Time extends Entidade {
  usuarioId: string;
  escopo: EscopoTime;
}
