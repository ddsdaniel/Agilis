import { AtorFK } from '../../pessoas/ator-fk';

export interface UserStory {
  id: string;
  nome: string;
  ator: AtorFK;
  narrativa: string;
  objetivo: string;
  historia: string;
  epicoId: string;
}
