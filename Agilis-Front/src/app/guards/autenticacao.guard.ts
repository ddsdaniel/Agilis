import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AutenticacaoService } from '../services/autenticacao.service';


@Injectable({
  providedIn: 'root'
})
export class AutenticacaoGuard implements CanActivate {

  constructor(
    private autenticacaoService: AutenticacaoService,
    private router: Router,
  ) { }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return this.verificarAcesso();
  }


  private verificarAcesso() {

    if (this.autenticacaoService.estaLogado()) {
      return true;
    }

    this.router.navigate(['/bem-vindo']);
    return false;

  }

}
