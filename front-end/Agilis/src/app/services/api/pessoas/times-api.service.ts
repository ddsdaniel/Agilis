import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Email } from 'src/app/models/pessoas/email';
import { Time } from 'src/app/models/pessoas/time';
import { UsuarioFK } from 'src/app/models/pessoas/usuario-FK';

import { CrudApiBaseService } from '../crud-api-base.service';

@Injectable({
  providedIn: 'root'
})
export class TimesApiService extends CrudApiBaseService<Time> {
  constructor(httpClient: HttpClient) {
    super(httpClient, 'times');
  }

  adicionarAdmin(timeId: string, email: Email): Observable<UsuarioFK> {
    return super.post<Email, UsuarioFK>(email, `${timeId}/administradores`);
  }

  excluirAdmin(timeId: string, adminId: string): Observable<void> {
    return super.delete(`${timeId}/administradores/${adminId}`);
  }

  adicionarColaborador(timeId: string, email: Email): Observable<UsuarioFK> {
    return super.post<Email, UsuarioFK>(email, `${timeId}/colaboradores`);
  }

  excluirColaborador(timeId: string, colabId: string): Observable<void> {
    return super.delete(`${timeId}/colaboradores/${colabId}`);
  }

}
