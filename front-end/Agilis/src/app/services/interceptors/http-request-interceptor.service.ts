import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from '@angular/common/http';
import { Injectable, NgZone } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

import { UsuarioApiService } from '../api/pessoas/usuario-api.service';
import { ProcessandoService } from '../processando.service';

@Injectable()
export class HttpsRequestInterceptorService implements HttpInterceptor {

  constructor(
    private router: Router,
    private ngZone: NgZone,
    private usuarioApiService: UsuarioApiService,
    private processandoService: ProcessandoService,
  ) { }

  intercept(httpRequest: HttpRequest<any>, httpHandler: HttpHandler): Observable<HttpEvent<any>> {

    if (this.usuarioApiService.usuarioLogado) {
      const logado = this.usuarioApiService.usuarioLogado;

      httpRequest = httpRequest.clone({
        setHeaders: { Authorization: `${logado.tipoToken} ${logado.token}` }
      });
    }

    this.processandoService.push();
    return httpHandler.handle(httpRequest)
      .pipe(
        tap(
          (event: HttpEvent<any>) => {

            // tratamento de requests com sucesso
            if (event instanceof HttpResponse) {
              this.processandoService.pop();
              console.log('sucesso');
            }

          },
          (err: any) => {

            this.processandoService.pop();

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
