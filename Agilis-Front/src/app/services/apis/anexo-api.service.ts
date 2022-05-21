import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Anexo } from 'src/app/models/anexo';

import { CrudApiBaseService } from './crud-api-base.service';

@Injectable({
  providedIn: 'root'
})
export class AnexoApiService extends CrudApiBaseService<Anexo> {
  constructor(httpClient: HttpClient) {
    super(httpClient, 'Anexo');
  }
}
