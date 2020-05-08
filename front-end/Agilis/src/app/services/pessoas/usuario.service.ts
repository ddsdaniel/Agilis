import { Injectable } from '@angular/core';
import { UsuarioLogado } from 'src/app/models/pessoas/usuario-logado';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

  usuarioLogado: UsuarioLogado;

  constructor() { }
}
