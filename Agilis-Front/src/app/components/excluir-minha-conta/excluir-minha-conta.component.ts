import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { Login } from 'src/app/models/login';
import { UsuarioApiService } from 'src/app/services/apis/usuario-api.service';
import { AutenticacaoService } from 'src/app/services/autenticacao.service';
import { LocalStorageService } from 'src/app/services/local-storage.service';
import { ProcessandoService } from 'src/app/services/processando.service';
import { TituloService } from 'src/app/services/titulo.service';

@Component({
  selector: 'app-excluir-minha-conta',
  templateUrl: './excluir-minha-conta.component.html',
  styleUrls: ['./excluir-minha-conta.component.scss']
})
export class ExcluirMinhaContaComponent implements OnInit {

  estouCiente = false;
  senha = '';

  constructor(
    private usuarioApiService: UsuarioApiService,
    private router: Router,
    private snackBar: MatSnackBar,
    private autenticacaoSerivce: AutenticacaoService,
    private localStorageService: LocalStorageService,
    private tituloService: TituloService,
    public processandoService: ProcessandoService,
  ) { }

  ngOnInit(): void {
    this.tituloService.definir('Excluir a minha conta');
  }

  excluir(): void {

    const login: Login = {
      email: this.autenticacaoSerivce.usuarioLogado.usuario.email,
      senha: this.senha
    };

    this.autenticacaoSerivce.autenticar(login)
      .pipe(
        switchMap(() => this.usuarioApiService.excluir())
      )
      .subscribe({
        next: () => {
          this.autenticacaoSerivce.limparUsuarioLogado();
          this.localStorageService.clear();
          this.snackBar.open('Conta excluída com sucesso.');
          this.router.navigate(['bem-vindo']);
        },
        error: (error: HttpErrorResponse) => this.snackBar.open(error.message)
      });

  }

}
