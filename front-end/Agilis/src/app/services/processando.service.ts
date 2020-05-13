import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ProcessandoService {

  private contador = 0;

  constructor() { }

  push() {
    this.contador++;
  }

  pop() {
    this.contador--;
  }

  estaCarregando() {
    return this.contador > 0;
  }
}
