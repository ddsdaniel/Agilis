import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Entidade } from 'src/app/models/entidade';

import { ConsultaApiBaseService } from './consulta-api-base.service';

export abstract class CrudApiBaseService<TEntity extends Entidade> extends ConsultaApiBaseService<TEntity> {

  constructor(http: HttpClient, recurso: string) {
    super(http, recurso);
  }

  adicionar(entity: TEntity): Observable<string> {
    return super.post<TEntity, TEntity>(entity)
    .pipe(
      map((result: TEntity) => result.id)
    );
  }

  alterar(id: string, entity: TEntity): Observable<void> {
    return super.put<TEntity, void>(entity, `${id}`);
  }

  excluir(id: string): Observable<void> {
    return super.delete(`${id}`);
  }

}
