import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from '@angular/common/http';
import { Injectable, NgZone } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { catchError, switchMap, tap } from 'rxjs/operators';
import { TratadorErrosService } from '../services/apis/tratador-erros.service';
import { AutenticacaoService } from '../services/autenticacao.service';
import { ProcessandoService } from '../services/processando.service';

@Injectable()
export class HttpsRequestInterceptorService implements HttpInterceptor {

  constructor(
    private router: Router,
    private ngZone: NgZone,
    private processandoService: ProcessandoService,
    private tratadorErrosService: TratadorErrosService,
    private autenticacaoService: AutenticacaoService,
  ) { }

  intercept(httpRequest: HttpRequest<any>, httpHandler: HttpHandler): Observable<HttpEvent<any>> {

    httpRequest = this.adicionarTokenAoRequest(httpRequest);

    this.processandoService.push();
    return httpHandler.handle(httpRequest)
      .pipe(
        catchError(error => {
          if (error.status === 401) {
            return this.autenticacaoService.renovarToken()
              .pipe(
                switchMap(() => this.intercept(httpRequest, httpHandler))
              );
          }
          return throwError(error);
        }),
        tap(
          (event: HttpEvent<any>) => this.tratarSucessoRequest(event),
          (err: any) => this.tratarErroRequest(err)
        )
      );
  }

  private tratarSucessoRequest(event: HttpEvent<any>) {
    if (event instanceof HttpResponse) {
      this.processandoService.pop();
    }
  }

  private tratarErroRequest(err: any) {

    this.processandoService.pop();

    if (!(err instanceof HttpErrorResponse)) {
      return;
    }

    if (err.status === 401 || err.status === 403) {
      this.autenticacaoService.limparUsuarioLogado();
      this.ngZone.run(() => this.router.navigate(['login']));
    } else {
      this.tratadorErrosService.padronizarHttpErrorResponse(err);
    }
  }

  private adicionarTokenAoRequest(httpRequest: HttpRequest<any>) {

    if (this.autenticacaoService.estaLogado()) {
      const logado = this.autenticacaoService.usuarioLogado;
      httpRequest = httpRequest.clone({
        setHeaders: { Authorization: `${logado.tipoToken} ${logado.token}` }
      });
    }

    return httpRequest;
  }
}
