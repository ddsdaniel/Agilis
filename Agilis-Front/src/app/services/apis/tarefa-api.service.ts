import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { FiltroTarefa } from 'src/app/models/tarefas/filtro-tarefa';
import { Tarefa } from 'src/app/models/tarefas/tarefa';

import { CrudApiBaseService } from './crud-api-base.service';


@Injectable({
  providedIn: 'root'
})
export class TarefaApiService extends CrudApiBaseService<Tarefa> {

  constructor(httpClient: HttpClient) {
    super(httpClient, 'Tarefa');
  }

  obterTags(): Observable<string[]> {
    return super.get<string[]>('tags');
  }

  filtrar(filtro: FiltroTarefa)
    : Observable<Tarefa[]> {

    const params = new HttpParams()
      .append('sprintId', filtro.sprintId)
      .append('relatorId', filtro.relatorId)
      .append('solucionadorId', filtro.solucionadorId)
      ;

    return super.get<Tarefa[]>('pesquisa', params);
  }

}
