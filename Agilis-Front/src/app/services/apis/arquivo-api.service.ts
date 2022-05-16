import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Arquivo } from 'src/app/models/arquivo';

import { CrudApiBaseService } from './crud-api-base.service';

@Injectable({
  providedIn: 'root'
})
export class ArquivoApiService extends CrudApiBaseService<Arquivo> {
  constructor(httpClient: HttpClient) {
    super(httpClient, 'Arquivo');
  }
}
