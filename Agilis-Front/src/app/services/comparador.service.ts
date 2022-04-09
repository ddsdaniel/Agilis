import { Injectable } from '@angular/core';
import { Entidade } from '../models/entidade';

@Injectable({
  providedIn: 'root'
})
export class ComparadorService {

  constructor() { }

  compareById(entidade1: Entidade, entidade2: Entidade): boolean {
    return entidade1 && entidade2 && entidade1.id === entidade2.id;
  }
  
}
