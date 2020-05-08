import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { ApiRestBaseService } from './api-rest-base.service';

@Injectable({
  providedIn: 'root'
})
export abstract class CrudBaseService<TEntity> extends ApiRestBaseService {

  constructor(http: HttpClient, recurso: string) {
    super(http, recurso);
  }

  obteUm(): Observable<TEntity> {
    return this.get<TEntity>();
  }

  obterSubrecurso(subrecurso: string): Observable<TEntity> {
    return this.get<any>(subrecurso);
  }

  pesquisar(filtro?: string): Observable<TEntity[]> {
    return this.get<TEntity[]>('', this.buildParams({ filtro }));
  }

}
