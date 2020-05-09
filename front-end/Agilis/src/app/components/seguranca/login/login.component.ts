import { Component, OnInit } from '@angular/core';
import { LoginDados } from 'src/app/models/seguranca/login-dados';
import { UsuarioApiService } from 'src/app/services/api/pessoas/usuario-api.service';
import { UsuarioLogado } from 'src/app/models/pessoas/usuario-logado';
import { HttpErrorResponse } from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar';
import { LocalStorageService } from 'src/app/services/local-storage.service';
import { localStorageKeys } from 'src/assets/constantes/local-storage-keys';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  login: LoginDados = {
    email: '',
    senha: ''
  };

  constructor(
    private usuarioApiService: UsuarioApiService,
    private snackBar: MatSnackBar,
    private localStorageService: LocalStorageService
  ) {

  }

  ngOnInit() {
    this.usuarioApiService.usuarioLogado = null;
    this.localStorageService.clear();
  }

  autenticar() {
    this.usuarioApiService.autenticar(this.login)
      .subscribe(
        (logado: UsuarioLogado) => this.atualizarUsuarioLogado(logado),
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

  atualizarUsuarioLogado(logado: UsuarioLogado): void {
    this.usuarioApiService.usuarioLogado = logado;
    this.localStorageService.setJSON<UsuarioLogado>(localStorageKeys.usuarioLogado, logado);
  }

}
