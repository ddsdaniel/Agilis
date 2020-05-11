import { Ator } from '../../pessoas/ator';

export interface UserStory {
  id: string;
  nome: string;
  ator: Ator;
  narrativa: string;
  objetivo: string;
  historia: string;
}
