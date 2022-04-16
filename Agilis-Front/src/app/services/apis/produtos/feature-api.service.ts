import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Feature } from 'src/app/models/produtos/feature';

import { CrudApiBaseService } from '../crud-api-base.service';

@Injectable({
  providedIn: 'root'
})
export class FeatureApiService extends CrudApiBaseService<Feature> {
  constructor(httpClient: HttpClient) {
    super(httpClient, 'Feature');
  }
}
