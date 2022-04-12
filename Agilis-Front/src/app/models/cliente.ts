import { Entidade } from './entidade';

export interface Cliente extends Entidade {
  nome: string;
  idIntegracaoSac: string;
}
