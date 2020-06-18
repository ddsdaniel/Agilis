import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Release } from 'src/app/models/trabalho/releases/release';

import { Observable } from 'rxjs';
import { ApiRestBaseService } from '../api-rest-base.service';
import { StringContainer } from 'src/app/models/string-container';
import { SprintFK } from 'src/app/models/trabalho/sprints/sprint-fk';

@Injectable({
  providedIn: 'root'
})
export class ReleasesApiService extends ApiRestBaseService {

  constructor(httpClient: HttpClient) {
    super(httpClient, 'releases');
  }

  renomear(timeId: string, releaseId: string, nome: string): Observable<void> {
    return super.patch<StringContainer, void>({ texto: nome }, `${timeId}/${releaseId}`);
  }

  obterUm(releaseId: string): Observable<Release> {
    return super.get<Release>(`${releaseId}`);
  }

  adicionarSprint(releaseId: string, nome: string): Observable<SprintFK> {
    return super.post<StringContainer, SprintFK>({ texto: nome }, `${releaseId}/sprints`);
  }

  excluirSprint(releaseId: string, sprintId: string): Observable<void> {
    return super.delete(`${releaseId}/sprints/${sprintId}`);
  }
}
