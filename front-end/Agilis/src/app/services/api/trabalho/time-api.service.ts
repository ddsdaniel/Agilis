import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Time } from 'src/app/models/trabalho/times/time';

import { CrudApiBaseService } from '../crud-api-base.service';

@Injectable({
  providedIn: 'root'
})
export class TimeApiService extends CrudApiBaseService<Time> {

  constructor(httpClient: HttpClient) {
    super(httpClient, 'Time');
  }
}
