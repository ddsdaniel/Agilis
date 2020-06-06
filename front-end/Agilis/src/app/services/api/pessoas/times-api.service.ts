import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Time } from 'src/app/models/pessoas/time';

import { CrudApiBaseService } from '../crud-api-base.service';
import { Email } from 'src/app/models/pessoas/email';
import { Observable } from 'rxjs';
import { UsuarioVO } from 'src/app/models/pessoas/usuario-vo';

@Injectable({
  providedIn: 'root'
})
export class TimesApiService extends CrudApiBaseService<Time> {

  constructor(httpClient: HttpClient) {
    super(httpClient, 'times');
  }

  adicionarAdmin(timeId: string, email: Email): Observable<UsuarioVO> {
    return super.post<Email, UsuarioVO>(email, `${timeId}/admin`);
  }

  excluirAdmin(timeId: string, adminId: string): Observable<void> {
    return super.delete(`${timeId}/admin/${adminId}`);
  }
}
