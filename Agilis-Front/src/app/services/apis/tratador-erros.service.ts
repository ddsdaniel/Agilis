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

    console.error(err);

    let mensagem = '';
    if (Array.isArray(err.error)) {
      mensagem = err.error.map(e => e.message).join(' ');
    } else if (err.error.title) {
      mensagem = err.error.title;
    } else if (err.error.message) {
      mensagem = err.error.message;
    } else {
      if (err.statusText === 'OK') {
        mensagem = err.error;
      } else {
        mensagem = err.statusText;
      }
    }

    mensagem = this.traduzir(mensagem);

    err['message' as any] = mensagem;
    err['statusText' as any] = err['message' as any];
  }

  private traduzir(mensagem: any): any {
    switch (mensagem) {
      case 'Unknown Error': return 'Erro desconhecido. Verifique se a API está rodando.';
      case 'One or more validation errors occurred.': return 'Uma ou mais validações falharam.';
      default: return mensagem;
    }
  }
}
