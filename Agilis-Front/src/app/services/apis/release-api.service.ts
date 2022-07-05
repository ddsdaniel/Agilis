import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Release } from 'src/app/models/release';
import { CrudApiBaseService } from './crud-api-base.service';


@Injectable({
  providedIn: 'root'
})
export class ReleaseApiService extends CrudApiBaseService<Release> {
  constructor(httpClient: HttpClient) {
    super(httpClient, 'Release');
  }
}
