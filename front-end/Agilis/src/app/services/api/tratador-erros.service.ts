import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TratadorErrosService {

  constructor() { }

  padronizarHttpErrorResponse(err: HttpErrorResponse) {
    if (!err.error) {
      return;
    }

    let mensagem = Array.isArray(err.error)
      ? err.error.map(e => e.message).join(' ')
      : (err.error.message ? err.error.message : err.statusText);

    mensagem = this.traduzir(mensagem);

    err['message' as any] = mensagem;
    err['statusText' as any] = err['message' as any];
  }

  private traduzir(mensagem: any): any {
    switch (mensagem) {
      case 'Unknown Error': return 'Erro desconhecido';
      default: return mensagem;
    }
  }
}
