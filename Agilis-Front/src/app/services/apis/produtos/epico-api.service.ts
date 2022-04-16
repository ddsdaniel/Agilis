import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Epico } from 'src/app/models/produtos/epico';

import { CrudApiBaseService } from '../crud-api-base.service';

@Injectable({
  providedIn: 'root'
})
export class EpicoApiService extends CrudApiBaseService<Epico> {
  constructor(httpClient: HttpClient) {
    super(httpClient, 'Epico');
  }
}
