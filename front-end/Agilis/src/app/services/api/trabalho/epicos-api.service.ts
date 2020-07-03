import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Epico } from 'src/app/models/trabalho/epicos/epico';

import { CrudApiBaseService } from '../crud-api-base.service';

@Injectable({
  providedIn: 'root'
})
export class EpicosApiService extends CrudApiBaseService<Epico> {
  constructor(httpClient: HttpClient) {
    super(httpClient, 'epicos');
  }

  pesquisarPorTema(filtro: string, temaId: string): Observable<Epico[]> {
    return super.get<Epico[]>('pesquisa-crud', this.buildParams({ filtro, temaId }));
  }
}
