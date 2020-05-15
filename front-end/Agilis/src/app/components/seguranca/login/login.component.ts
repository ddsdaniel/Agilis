import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { LoginDados } from 'src/app/models/seguranca/login-dados';
import { UsuariosApiService } from 'src/app/services/api/pessoas/usuarios-api.service';
import { LocalStorageService } from 'src/app/services/local-storage.service';
import { AutenticacaoService } from 'src/app/services/seguranca/autenticacao.service';

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
    private usuariosApiService: UsuariosApiService,
    private snackBar: MatSnackBar,
    private localStorageService: LocalStorageService,
    private router: Router,
    private autenticacaoService: AutenticacaoService,
  ) {

  }

  ngOnInit() {
    this.autenticacaoService.usuarioLogado = null;
    this.localStorageService.clear();
  }

  autenticar() {
    this.autenticacaoService.autenticar(this.login)
      .subscribe(
        () => this.router.navigate(['times']),
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

  cadastrarSe() {
    this.router.navigate(['usuarios/new']);
  }
}
