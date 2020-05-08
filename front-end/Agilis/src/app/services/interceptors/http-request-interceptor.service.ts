import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from '@angular/common/http';
import { Injectable, NgZone } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

import { UsuarioService } from '../pessoas/usuario.service';

@Injectable()
export class HttpsRequestInterceptorService implements HttpInterceptor {

  constructor(
    private router: Router,
    private ngZone: NgZone,
    private usuarioService: UsuarioService,
  ) { }

  intercept(httpRequest: HttpRequest<any>, httpHandler: HttpHandler): Observable<HttpEvent<any>> {

    if (this.usuarioService.usuarioLogado) {
      const logado = this.usuarioService.usuarioLogado;

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
              if (err.status === 401 && !this.usuarioService.usuarioLogado) {
                this.ngZone.run(() => this.router.navigate(['login']));
              }
            }
          })
      );
  }
}
