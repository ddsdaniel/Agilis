import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Login } from 'src/app/models/login';
import { AutenticacaoService } from 'src/app/services/autenticacao.service';
import { LocalStorageService } from 'src/app/services/local-storage.service';
import { ProcessandoService } from 'src/app/services/processando.service';
import { TituloService } from 'src/app/services/titulo.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  login: Login = {
    email: '',
    senha: '',
  };

  constructor(
    private router: Router,
    private autenticacaoService: AutenticacaoService,
    private snackBar: MatSnackBar,
    private localStorageService: LocalStorageService,
    private tituloService: TituloService,
    public processandoService: ProcessandoService,
  ) { }

  ngOnInit() {
    this.tituloService.definir('Login');

    this.autenticacaoService.limparUsuarioLogado();
    this.localStorageService.clear();
  }

  entrar() {
    this.autenticacaoService.autenticar(this.login)
      .subscribe(
        () => this.router.navigate(['tarefas']),
        (error: HttpErrorResponse) => this.snackBar.open(error.message));
  }

}
