import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';


/**
 * Classe para ser utilizada pelos serviços que interagem com a API REST do PDV
 */
export abstract class ApiRestBaseService {

    private readonly recurso: string;

    /**
     * URL da API concatenada com o recurso declarado no contrutor
     */
    protected getUrl(subRecurso?: string): string {
        return `${environment.apiUrl}/${this.recurso}${subRecurso ? `/${subRecurso}` : ''}`;
    }

    constructor(
        protected http: HttpClient,
        recurso: string
    ) {
        this.recurso = recurso;
    }

    /**
     * Executa o método GET no recurso desse service
     *
     * @param subRecurso caminho adicional no final da URL padrão do service
     * @param params parametros de URL -> use o método buildParams(object)
     * @typeparam TRetorno  tipo de objeto retornado
     */
    protected get<TRetorno>(subRecurso?: string, params?: HttpParams): Observable<TRetorno> {
        return this.http.get<TRetorno>(this.getUrl(subRecurso), { params });
    }

    /**
     * Executa o método POST no recurso desse service
     *
     * @param body objeto que será enviado
     * @param subRecurso caminho adicional no final da URL padrão do service
     * @param params parametros de URL -> use o método buildParams(object)
     * @typeparam TEnvio tipo de objeto enviado
     * @typeparam TRetorno  tipo de objeto retornado
     */
    protected post<TEnvio, TRetorno>(body: TEnvio, subRecurso?: string, params?: HttpParams): Observable<TRetorno> {
        if (body === null) {
            body = {} as any;
        }
        return this.http.post<TRetorno>(this.getUrl(subRecurso), body, { params });
    }

    /**
     * Executa o método PATCH (atualizar parcialmente) no recurso desse service
     *
     * @param body objeto que será enviado
     * @param subRecurso caminho adicional no final da URL padrão do service
     * @param params parametros de URL -> use o método buildParams(object)
     * @typeparam TEnvio tipo de objeto enviado
     * @typeparam TRetorno  tipo de objeto retornado
     */
    protected patch<TEnvio, TRetorno>(body: TEnvio, subRecurso?: string, params?: HttpParams): Observable<TRetorno> {
        if (body == null) {
            body = {} as any;
        }
        return this.http.patch<TRetorno>(this.getUrl(subRecurso), body, { params });
    }

    /**
     * Executa o método DELETE no recurso desse service
     *
     * @param subRecurso caminho adicional no final da URL padrão do service
     * @typeparam TRetorno  tipo de objeto retornado
     */
    protected delete<TRetorno>(subRecurso?: string): Observable<TRetorno> {
        return this.http.delete<TRetorno>(this.getUrl(subRecurso));
    }

    protected buildParams(object: any) {
        return new HttpParams({
            fromObject: object
        });
    }
}
