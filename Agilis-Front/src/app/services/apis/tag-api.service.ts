import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Tag } from 'src/app/models/tags/tag';

import { ConsultaApiBaseService } from './consulta-api-base.service';


@Injectable({
  providedIn: 'root'
})
export class TagApiService extends ConsultaApiBaseService<Tag> {
  constructor(httpClient: HttpClient) {
    super(httpClient, 'Tag');
  }
}
