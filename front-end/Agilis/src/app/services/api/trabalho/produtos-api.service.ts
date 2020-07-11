import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { StringContainer } from 'src/app/models/string-container';
import { Epico } from 'src/app/models/trabalho/epicos/epico';
import { Produto } from 'src/app/models/trabalho/produtos/produto';
import { Tema } from 'src/app/models/trabalho/temas/tema';
import { UserStoryFK } from 'src/app/models/trabalho/user-stories/user-story-fk';

import { CrudApiBaseService } from '../crud-api-base.service';

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

  adicionarTema(produtoId: string, nome: string): Observable<Tema> {
    return super.post<StringContainer, Tema>({ texto: nome }, `${produtoId}/temas`);
  }

  adicionarEpico(produtoId: string, temaId: string, nome: string): Observable<Epico> {
    return super.post<StringContainer, Epico>({ texto: nome }, `${produtoId}/temas/${temaId}/epicos`);
  }

  adicionarUserStory(produtoId: string, temaId: string, epicoId: string, nome: string): Observable<UserStoryFK> {
    const url = `${produtoId}/temas/${temaId}/epicos/${epicoId}/user-stories`;
    return super.post<StringContainer, UserStoryFK>({ texto: nome }, url);
  }
}
