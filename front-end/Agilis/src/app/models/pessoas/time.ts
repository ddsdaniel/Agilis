import { EntidadeFavorita } from '../entidade-favorita';
import { EscopoTime } from 'src/app/enums/pessoas/escopo-time.enum';

export interface Time extends EntidadeFavorita {
  usuarioId: string;
  escopo: EscopoTime;
}
