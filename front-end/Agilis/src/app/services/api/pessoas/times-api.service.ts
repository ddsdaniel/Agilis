﻿import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Time } from 'src/app/models/pessoas/time';

import { CrudApiBaseService } from '../crud-api-base.service';

@Injectable({
  providedIn: 'root'
})
export class TimesApiService extends CrudApiBaseService<Time> {

  constructor(httpClient: HttpClient) {
    super(httpClient, 'times');
  }

}
