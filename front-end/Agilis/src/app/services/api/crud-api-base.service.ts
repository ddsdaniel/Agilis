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

  obterUm(id: string): Observable<TEntity> {
    return this.get<TEntity>(`${id}`);
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

  alterar(id: string, entity: TEntity): Observable<void> {
    return super.put<TEntity, void>(entity, `${id}`);
  }

}
