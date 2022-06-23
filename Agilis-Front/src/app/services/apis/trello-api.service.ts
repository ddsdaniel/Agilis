import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { ImportacaoTrello } from 'src/app/models/tarefas/importacao-trello';
import { ApiRestBaseService } from './api-rest-base.service';

@Injectable({
  providedIn: 'root'
})
export class TrelloApiService extends ApiRestBaseService {

  constructor(httpClient: HttpClient) {
    super(httpClient, 'Trello');
  }

  importar(importacao: ImportacaoTrello): Observable<void> {
    return super.post<ImportacaoTrello, void>(importacao);
  }

}
