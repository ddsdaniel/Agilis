import { UserStorySM } from './user-story-sm';

export interface EpicoSM {
  nome: string;
  userStories: UserStorySM[];
}
