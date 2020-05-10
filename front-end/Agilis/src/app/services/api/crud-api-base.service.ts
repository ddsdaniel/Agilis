import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { ApiRestBaseService } from './api-rest-base.service';

@Injectable({
  providedIn: 'root'
})
export abstract class CrudApiBaseService<TEntity> extends ApiRestBaseService {

  constructor(http: HttpClient, recurso: string) {
    super(http, recurso);
  }

  obteUm(): Observable<TEntity> {
    // TODO: passar o id
    return this.get<TEntity>();
  }

  obteTodos(): Observable<TEntity[]> {
    return this.get<TEntity[]>();
  }

  obterSubrecurso(subrecurso: string): Observable<TEntity> {
    return this.get<any>(subrecurso);
  }

  pesquisar(filtro?: string): Observable<TEntity[]> {
    return this.get<TEntity[]>('', this.buildParams({ filtro }));
  }

  adicionar(entity: TEntity): Observable<string> {
    return super.post<TEntity, string>(entity);
  }

}
