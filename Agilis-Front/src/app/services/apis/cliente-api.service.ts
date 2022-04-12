import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Cliente } from 'src/app/models/cliente';
import { CrudApiBaseService } from './crud-api-base.service';


@Injectable({
  providedIn: 'root'
})
export class ClienteApiService extends CrudApiBaseService<Cliente> {
  constructor(httpClient: HttpClient) {
    super(httpClient, 'Cliente');
  }
}
