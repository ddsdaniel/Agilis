import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Produto } from 'src/app/models/trabalho/produtos/produto';

import { CrudApiBaseService } from '../crud-api-base.service';

@Injectable({
  providedIn: 'root'
})
export class ProdutoApiService extends CrudApiBaseService<Produto> {

  constructor(httpClient: HttpClient) {
    super(httpClient, 'Produto');
  }
}
