import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { StringContainer } from 'src/app/models/string-container';
import { Fase } from 'src/app/models/trabalho/fase';
import { Jornada } from 'src/app/models/trabalho/produtos/jornada';
import { Produto } from 'src/app/models/trabalho/produtos/produto';

import { ApiRestBaseService } from '../api-rest-base.service';

@Injectable({
  providedIn: 'root'
})
export class ProdutosApiService extends ApiRestBaseService {
  constructor(httpClient: HttpClient) {
    super(httpClient, 'produtos');
  }

  renomear(timeId: string, produtoId: string, nome: string): Observable<void> {
    return super.patch<StringContainer, void>({ texto: nome }, `${timeId}/${produtoId}`);
  }

  obterUm(produtoId: string): Observable<Produto> {
    return super.get<Produto>(`${produtoId}`);
  }

  adicionarJornada(produtoId: string, jornada: Jornada): Observable<Jornada> {
    return super.post<Jornada, Jornada>(jornada, `${produtoId}/jornadas`);
  }

  excluirJornada(produtoId: string, posicao: number): Observable<void> {
    return super.delete(`${produtoId}/jornadas/${posicao}`);
  }

  obterJornada(produtoId: string, posicao: number): Observable<Jornada> {
    return super.get<Jornada>(`${produtoId}/jornadas/${posicao}`);
  }

  renomearJornada(produtoId: string, posicao: number, nome: string): Observable<void> {
    return super.patch<StringContainer, void>({ texto: nome }, `${produtoId}/jornadas/${posicao}`);
  }

  adicionarFase(produtoId: string, posicaoJornada: number, nome: string): Observable<Fase> {
    return super.post<StringContainer, Fase>({ texto: nome }, `${produtoId}/jornadas/${posicaoJornada}/fases`);
  }
}
