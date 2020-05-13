import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { EntidadeFavorita } from 'src/app/models/entidade-favorita';
import { Favorito } from 'src/app/models/favorito';

import { CrudApiBaseService } from './crud-api-base.service';

@Injectable({
  providedIn: 'root'
})
export class CrudFavoritoApiBaseService<TEntity extends EntidadeFavorita> extends CrudApiBaseService<TEntity> {

  constructor(http: HttpClient, recurso: string) {
    super(http, recurso);
  }

  favoritar(id: string, favorito: Favorito): Observable<void> {
    return super.patch<Favorito, void>(favorito, `${id}/favorito`);
  }

}
