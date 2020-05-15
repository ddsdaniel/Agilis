import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Usuario } from 'src/app/models/pessoas/usuario';
import { UsuarioCadastro } from 'src/app/models/pessoas/usuario-cadastro';
import { UsuarioLogado } from 'src/app/models/pessoas/usuario-logado';
import { LoginDados } from 'src/app/models/seguranca/login-dados';

import { CrudApiBaseService } from '../crud-api-base.service';


@Injectable({
  providedIn: 'root'
})
export class UsuariosApiService extends CrudApiBaseService<Usuario> {

  constructor(httpClient: HttpClient) {
    super(httpClient, 'usuarios');
  }

  autenticar(login: LoginDados): Observable<UsuarioLogado> {
    return super.post<LoginDados, UsuarioLogado>(login, 'login');
  }

  cadastrar(usuario: UsuarioCadastro): Observable<string> {
    return super.post<UsuarioCadastro, string>(usuario);
  }
}
