import { Entidade } from '../../entidade';
import { UserStoryFK } from '../user-stories/user-story-fk';

export interface Epico  {
  id: string;
  nome: string;
  userStories: UserStoryFK[];
}
