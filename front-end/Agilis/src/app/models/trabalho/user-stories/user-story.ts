import { AtorFK } from '../../pessoas/ator-fk';
import { Entidade } from '../../entidade';
import { CriterioAceitacao } from './criterio-aceitacao';

export interface UserStory extends Entidade {
  ator: AtorFK;
  narrativa: string;
  objetivo: string;
  historia: string;
  epicoId: string;
  criteriosAceitacao: CriterioAceitacao[];
}
