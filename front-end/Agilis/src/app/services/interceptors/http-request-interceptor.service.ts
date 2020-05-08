import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from '@angular/common/http';
import { Injectable, NgZone } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

import { UsuarioApiService } from '../api/pessoas/usuario-api.service';

@Injectable()
export class HttpsRequestInterceptorService implements HttpInterceptor {

  constructor(
    private router: Router,
    private ngZone: NgZone,
    private usuarioApiService: UsuarioApiService,
  ) { }

  intercept(httpRequest: HttpRequest<any>, httpHandler: HttpHandler): Observable<HttpEvent<any>> {

    if (this.usuarioApiService.usuarioLogado) {
      const logado = this.usuarioApiService.usuarioLogado;

      httpRequest = httpRequest.clone({
        setHeaders: { Authorization: `${logado.tipoToken} ${logado.token}` }
      });
    }

    return httpHandler.handle(httpRequest)
      .pipe(
        tap(
          (event: HttpEvent<any>) => {
            if (event instanceof HttpResponse) {
              // tratamento de requests com sucesso
            }
          },
          (err: any) => {
            if (err instanceof HttpErrorResponse) {
              if (err.status === 401 && !this.usuarioApiService.usuarioLogado) {
                this.ngZone.run(() => this.router.navigate(['login']));
              } else {

                if (err.error) {
                  err['message' as any] = Array.isArray(err.error)
                    ? err.error.map(e => e.message).join(' ')
                    : (err.error.message ? err.error.message : err.statusText);
                  err['statusText' as any] = err['message' as any];
                }
              }
            }
          })
      );
  }
}
