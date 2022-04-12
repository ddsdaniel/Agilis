import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Sprint } from 'src/app/models/sprint';
import { CrudApiBaseService } from './crud-api-base.service';


@Injectable({
  providedIn: 'root'
})
export class SprintApiService extends CrudApiBaseService<Sprint> {
  constructor(httpClient: HttpClient) {
    super(httpClient, 'Sprint');
  }
}
