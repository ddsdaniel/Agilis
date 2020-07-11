﻿import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { StringContainer } from 'src/app/models/string-container';
import { Epico } from 'src/app/models/trabalho/epicos/epico';
import { Produto } from 'src/app/models/trabalho/produtos/produto';
import { Tema } from 'src/app/models/trabalho/temas/tema';
import { UserStoryFK } from 'src/app/models/trabalho/user-stories/user-story-fk';

import { CrudApiBaseService } from '../crud-api-base.service';
import { OrigemDestino } from 'src/app/models/origem-destino';

@Injectable({
  providedIn: 'root'
})
export class ProdutosApiService extends CrudApiBaseService<Produto> {

  constructor(httpClient: HttpClient) {
    super(httpClient, 'produtos');
  }

  pesquisarPorTime(filtro: string, timeId: string): Observable<Produto[]> {
    return super.get<Produto[]>('pesquisa-crud', this.buildParams({ filtro, timeId }));
  }

  adicionarTema(produtoId: string, tema: Tema): Observable<Tema> {
    return super.post<Tema, Tema>(tema, `${produtoId}/temas`);
  }

  adicionarEpico(produtoId: string, temaId: string, nome: string): Observable<Epico> {
    return super.post<StringContainer, Epico>({ texto: nome }, `${produtoId}/temas/${temaId}/epicos`);
  }

  adicionarUserStory(produtoId: string, temaId: string, epicoId: string, nome: string): Observable<UserStoryFK> {
    const url = `${produtoId}/temas/${temaId}/epicos/${epicoId}/user-stories`;
    return super.post<StringContainer, UserStoryFK>({ texto: nome }, url);
  }

  renomearTema(produtoId: string, temaId: string, nome: string): Observable<void> {
    return super.patch<StringContainer, void>({ texto: nome }, `${produtoId}/temas/${temaId}/renomear`);
  }

  excluirTema(produtoId: string, temaId: string): Observable<void> {
    return super.delete(`${produtoId}/temas/${temaId}`);
  }

  moverUserStory(
    produtoId: string,
    temaId: string,
    epicoId: string,
    userStoryId: string,
    origemDestino: OrigemDestino
  ): Observable<void> {
    const url = `${produtoId}/temas/${temaId}/epicos/${epicoId}/user-stories/${userStoryId}/mover`;
    return super.patch<OrigemDestino, void>(origemDestino, url);
  }

  moverTema(produtoId: string, temaId: string, origemDestino: OrigemDestino): Observable<void> {
    const url = `${produtoId}/temas/${temaId}/mover`;
    return super.patch<OrigemDestino, void>(origemDestino, url);
  }

}
