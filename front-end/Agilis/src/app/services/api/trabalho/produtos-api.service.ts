import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Produto } from 'src/app/models/trabalho/produtos/produto';

import { CrudFavoritoApiBaseService } from '../crud-favorito-api-base.service';

@Injectable({
  providedIn: 'root'
})
export class ProdutosApiService extends CrudFavoritoApiBaseService<Produto> {

  constructor(httpClient: HttpClient) {
    super(httpClient, 'produtos');
  }
}
