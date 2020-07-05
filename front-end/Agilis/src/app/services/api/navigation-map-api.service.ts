import { Injectable } from '@angular/core';
import { ApiRestBaseService } from './api-rest-base.service';
import { EntidadeNodo } from 'src/app/models/entidade-nodo';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class NavigationMapApiService extends ApiRestBaseService {

  constructor(httpClient: HttpClient) {
    super(httpClient, 'NavigationMap');
  }

  obterUm(): Observable<EntidadeNodo> {
    return super.get<EntidadeNodo>();
  }
}
