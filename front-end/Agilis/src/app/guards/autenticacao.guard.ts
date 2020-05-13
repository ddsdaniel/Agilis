import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { UsuarioApiService } from '../services/api/pessoas/usuario-api.service';

@Injectable({
  providedIn: 'root'
})
export class AutenticacaoGuard implements CanActivate {

  constructor(
    private autenticacaoService: UsuarioApiService,
    private router: Router
  ) { }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return this.verificarAcesso();
  }


  private verificarAcesso() {

    if (this.autenticacaoService.usuarioLogado) {
      return true;
    }

    this.router.navigate(['/login']);
    return false;

  }

}
