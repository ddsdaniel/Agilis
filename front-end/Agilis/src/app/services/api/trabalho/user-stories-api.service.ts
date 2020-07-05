import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserStory } from 'src/app/models/trabalho/user-stories/user-story';

import { CrudApiBaseService } from '../crud-api-base.service';

@Injectable({
  providedIn: 'root'
})
export class UserStoriesApiService extends CrudApiBaseService<UserStory> {

  constructor(httpClient: HttpClient) {
    super(httpClient, 'UserStories');
  }

  pesquisarPorEpico(filtro: string, epicoId: string): Observable<UserStory[]> {
    return super.get<UserStory[]>('pesquisa-crud', this.buildParams({ filtro, epicoId }));
  }
}
