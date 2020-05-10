import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Ator } from 'src/app/models/pessoas/ator';

import { CrudApiBaseService } from '../crud-api-base.service';

@Injectable({
  providedIn: 'root'
})
export class AtorApiService extends CrudApiBaseService<Ator> {

  constructor(httpClient: HttpClient) {
    super(httpClient, 'Ator');
  }
}
