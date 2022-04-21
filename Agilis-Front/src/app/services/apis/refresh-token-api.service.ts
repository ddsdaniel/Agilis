import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { RefreshToken } from 'src/app/models/seguranca/refresh-token';
import { UsuarioLogado } from 'src/app/models/seguranca/usuario-logado';

import { ApiRestBaseService } from './api-rest-base.service';


@Injectable({
  providedIn: 'root'
})
export class RefreshTokenApiService extends ApiRestBaseService {

  constructor(httpClient: HttpClient) {
    super(httpClient, 'RefreshToken');
  }

  renovar(refreshToken: RefreshToken): Observable<UsuarioLogado> {
    return super.post<RefreshToken, UsuarioLogado>(refreshToken);
  }

  testar(): Observable<void> {
    return super.get<void>('testar');
  }

}
