import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Entidade } from 'src/app/models/entidade';
import { ApiRestBaseService } from './api-rest-base.service';

export abstract class ConsultaApiBaseService<TEntity extends Entidade> extends ApiRestBaseService {

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

}
