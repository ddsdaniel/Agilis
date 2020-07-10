import { Entidade } from '../../entidade';
import { AtorFK } from '../../pessoas/ator-fk';
import { StoryMapping } from '../stories-mapping/story-mapping';

export interface Produto extends Entidade {
  timeId: string;
  atores: AtorFK[];
  storyMapping: StoryMapping;
}
