import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Dispositivo } from 'src/app/models/dispositivo';
import { CrudApiBaseService } from './crud-api-base.service';


@Injectable({
  providedIn: 'root'
})
export class DispositivoApiService extends CrudApiBaseService<Dispositivo> {
  constructor(httpClient: HttpClient) {
    super(httpClient, 'Dispositivo');
  }
}
