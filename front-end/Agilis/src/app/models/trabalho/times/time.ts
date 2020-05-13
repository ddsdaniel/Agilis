import { Usuario } from '../../pessoas/usuario';

export interface Time {
  id: string;
  nome: string;
  usuarioId: string;
  favorito: boolean;
}
