import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { RedefinicaoSenha } from 'src/app/models/seguranca/redefinicao-senha';
import { UsuarioApiService } from 'src/app/services/apis/usuario-api.service';
import { AutenticacaoService } from 'src/app/services/autenticacao.service';
import { ProcessandoService } from 'src/app/services/processando.service';
import { TituloService } from 'src/app/services/titulo.service';

@Component({
  selector: 'app-redefinir-minha-senha',
  templateUrl: './redefinir-minha-senha.component.html',
  styleUrls: ['./redefinir-minha-senha.component.scss']
})
export class RedefinirMinhaSenhaComponent implements OnInit {

  email: string;
  chave: string;
  redefinicaoSenha: RedefinicaoSenha = {
    novaSenha: '',
    confirmaSenha: '',
  };

  constructor(
    private tituloService: TituloService,
    private usuarioApiService: UsuarioApiService,
    private snackBar: MatSnackBar,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private autenticacaoService: AutenticacaoService,
    public processandoService: ProcessandoService,
  ) { }

  ngOnInit(): void {
    this.tituloService.definir('Redefinir a Minha Senha');

    this.activatedRoute.params
      .subscribe({
        next: () => {
          this.email = this.activatedRoute.snapshot.paramMap.get('email');
          this.chave = this.activatedRoute.snapshot.paramMap.get('chave');
        },
        error: (error: HttpErrorResponse) => this.snackBar.open(error.message)
      });
  }

  salvar() {
    this.usuarioApiService.redefinirMinhaSenha(this.email, this.chave, this.redefinicaoSenha)
      .subscribe({
        next: usuarioLogado => {
          this.autenticacaoService.persistirUsuarioLogado(usuarioLogado);
          this.snackBar.open('Senha redefinida');
          this.router.navigate(['transacoes']);
        },
        error: (error: HttpErrorResponse) => this.snackBar.open(error.message)
      });
  }
}
