import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { AlterarMinhaSenha } from 'src/app/models/alterar-minha-senha';
import { Email } from 'src/app/models/email';
import { Login } from 'src/app/models/login';
import { RedefinicaoSenha } from 'src/app/models/redefinicao-senha';
import { RefreshToken } from 'src/app/models/refresh-token';
import { UsuarioCadastro } from 'src/app/models/usuario-cadastro';
import { UsuarioConsulta } from 'src/app/models/usuario-consulta';
import { UsuarioLogado } from 'src/app/models/usuario-logado';
import { ApiRestBaseService } from './api-rest-base.service';


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

  cadastrarDadosPadroes(): Observable<void> {
    return super.post<any, void>(null, 'dados-padroes');
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

  redefinirMinhaSenha(email: string, chave: string, redefinicaoSenha: RedefinicaoSenha): Observable<UsuarioLogado> {
    return super.put<RedefinicaoSenha, UsuarioLogado>(redefinicaoSenha, `redefinir-minha-senha/${email}/${chave}`);
  }
}
