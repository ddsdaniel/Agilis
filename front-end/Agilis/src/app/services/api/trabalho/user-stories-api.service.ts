import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserStory } from 'src/app/models/trabalho/user-stories/user-story';

import { CrudApiBaseService } from '../crud-api-base.service';

@Injectable({
  providedIn: 'root'
})
export class UserStoryApiService extends CrudApiBaseService<UserStory> {

  constructor(httpClient: HttpClient) {
    super(httpClient, 'UserStories');
  }
}
