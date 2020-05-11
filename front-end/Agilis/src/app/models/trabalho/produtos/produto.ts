import { Usuario } from '../../pessoas/usuario';

export interface Produto {
  id: string;
  nome: string;
  usuarioId: string;
}
