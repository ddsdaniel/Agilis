import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';


export abstract class ApiRestBaseService {

  private readonly recurso: string;

  protected getUrl(subRecurso?: string): string {
    return `${environment.apiUrl}/${this.recurso}${subRecurso ? `/${subRecurso}` : ''}`;
  }

  constructor(
    protected http: HttpClient,
    recurso: string
  ) {
    this.recurso = recurso;
  }

  protected get<TRetorno>(subRecurso?: string, params?: HttpParams): Observable<TRetorno> {
    return this.http.get<TRetorno>(this.getUrl(subRecurso), { params });
  }

  protected post<TEnvio, TRetorno>(body: TEnvio, subRecurso?: string, params?: HttpParams): Observable<TRetorno> {
    if (body === null) {
      body = {} as any;
    }
    return this.http.post<TRetorno>(this.getUrl(subRecurso), body, { params });
  }

  protected patch<TEnvio, TRetorno>(body: TEnvio, subRecurso?: string, params?: HttpParams): Observable<TRetorno> {
    if (body == null) {
      body = {} as any;
    }
    return this.http.patch<TRetorno>(this.getUrl(subRecurso), body, { params });
  }

  protected put<TEnvio, TRetorno>(body: TEnvio, subRecurso?: string, params?: HttpParams): Observable<TRetorno> {
    if (body == null) {
      body = {} as any;
    }
    return this.http.put<TRetorno>(this.getUrl(subRecurso), body, { params });
  }

  protected delete(subRecurso?: string): Observable<void> {
    return this.http.delete<void>(this.getUrl(subRecurso));
  }

  protected buildParams(object: any) {
    return new HttpParams({
      fromObject: object
    });
  }
}
