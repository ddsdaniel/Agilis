import { ItemSelect } from '../item-select';

export interface DialogoSelect {
  titulo: string;
  label: string;
  lista: ItemSelect[];
  idAtual: number;
}
