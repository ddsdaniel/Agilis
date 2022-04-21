import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { AlterarMinhaSenha } from 'src/app/models/seguranca/alterar-minha-senha';
import { Email } from 'src/app/models/email';
import { ApiRestBaseService } from './api-rest-base.service';
import { Login } from 'src/app/models/seguranca/login';
import { RedefinicaoSenha } from 'src/app/models/seguranca/redefinicao-senha';
import { UsuarioCadastro } from 'src/app/models/seguranca/usuario-cadastro';
import { UsuarioConsulta } from 'src/app/models/seguranca/usuario-consulta';
import { UsuarioLogado } from 'src/app/models/seguranca/usuario-logado';


@Injectable({
  providedIn: 'root'
})
export class UsuarioApiService extends ApiRestBaseService {

  constructor(httpClient: HttpClient) {
    super(httpClient, 'usuario');
  }

  adicionar(entity: UsuarioCadastro): Observable<UsuarioConsulta> {
    return super.post<UsuarioCadastro, UsuarioConsulta>(entity);
  }

  autenticar(login: Login): Observable<UsuarioLogado> {
    return super.post<Login, UsuarioLogado>(login, 'login');
  }

  excluir(): Observable<void> {
    return super.delete('conta');
  }

  alterarMinhaSenha(alterarMinhaSenha: AlterarMinhaSenha): Observable<void> {
    return super.put<AlterarMinhaSenha, void>(alterarMinhaSenha, 'senha');
  }

  esqueciMinhaSenha(email: Email): Observable<void> {
    return super.post<Email, void>(email, 'esqueci-minha-senha');
  }

  obterTodos(): Observable<UsuarioConsulta[]> {
    return super.get<UsuarioConsulta[]>();
  }

  redefinirMinhaSenha(email: string, chave: string, redefinicaoSenha: RedefinicaoSenha): Observable<UsuarioLogado> {
    return super.put<RedefinicaoSenha, UsuarioLogado>(redefinicaoSenha, `redefinir-minha-senha/${email}/${chave}`);
  }
}
