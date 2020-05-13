import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Time } from 'src/app/models/trabalho/times/time';

import { CrudApiBaseService } from '../crud-api-base.service';
import { Observable } from 'rxjs';
import { Favorito } from 'src/app/models/favorito';

@Injectable({
  providedIn: 'root'
})
export class TimeApiService extends CrudApiBaseService<Time> {

  constructor(httpClient: HttpClient) {
    super(httpClient, 'Time');
  }

  favoritar(id: string, favorito: Favorito): Observable<void> {
    return super.patch<Favorito, void>(favorito, `${id}/favorito`);
  }
}
