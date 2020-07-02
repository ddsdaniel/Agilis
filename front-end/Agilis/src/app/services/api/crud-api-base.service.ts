import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { ApiRestBaseService } from './api-rest-base.service';
import { map } from 'rxjs/operators';
import { Entidade } from 'src/app/models/entidade';

@Injectable({
  providedIn: 'root'
})
export abstract class CrudApiBaseService<TEntity extends Entidade> extends ApiRestBaseService {

  constructor(http: HttpClient, recurso: string) {
    super(http, recurso);
  }

  obterUm(id: string): Observable<TEntity> {
    return super.get<TEntity>(`${id}`);
  }

  obterTodos(): Observable<TEntity[]> {
    return super.get<TEntity[]>();
  }

  obterSubrecurso(subrecurso: string): Observable<TEntity> {
    return super.get<any>(subrecurso);
  }

  pesquisar(filtro?: string): Observable<TEntity[]> {
    return super.get<TEntity[]>('pesquisa', this.buildParams({ filtro }));
  }

  adicionar(entity: TEntity): Observable<string> {
    return super.post<TEntity, TEntity>(entity)
    .pipe(
      map((result: TEntity) => result.id)
    );
  }

  alterar(id: string, entity: TEntity): Observable<void> {
    return super.put<TEntity, void>(entity, `${id}`);
  }

  excluir(id: string): Observable<void> {
    return super.delete(`${id}`);
  }

}
