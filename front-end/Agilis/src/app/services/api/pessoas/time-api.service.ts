import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Time } from 'src/app/models/pessoas/time';

import { CrudFavoritoApiBaseService } from '../crud-favorito-api-base.service';

@Injectable({
  providedIn: 'root'
})
export class TimeApiService extends CrudFavoritoApiBaseService<Time> {

  constructor(httpClient: HttpClient) {
    super(httpClient, 'Time');
  }

}
