import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Tag } from 'src/app/models/tags/tag';
import { CrudApiBaseService } from './crud-api-base.service';

@Injectable({
  providedIn: 'root'
})
export class TagApiService extends CrudApiBaseService<Tag> {
  constructor(httpClient: HttpClient) {
    super(httpClient, 'Tag');
  }
}
