import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Tarefa } from 'src/app/models/tarefa';
import { CrudApiBaseService } from './crud-api-base.service';


@Injectable({
  providedIn: 'root'
})
export class TarefaApiService extends CrudApiBaseService<Tarefa> {
  constructor(httpClient: HttpClient) {
    super(httpClient, 'Tarefa');
  }
}
