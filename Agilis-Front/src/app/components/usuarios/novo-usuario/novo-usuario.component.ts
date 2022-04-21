import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { constantes } from 'src/app/consts/constantes';
import { RegraUsuario } from 'src/app/enums/regra-usuario.enum';
import { UsuarioCadastro } from 'src/app/models/seguranca/usuario-cadastro';
import { UsuarioApiService } from 'src/app/services/apis/usuario-api.service';
import { AutenticacaoService } from 'src/app/services/autenticacao.service';
import { ProcessandoService } from 'src/app/services/processando.service';
import { TituloService } from 'src/app/services/titulo.service';

@Component({
  selector: 'app-novo-usuario',
  templateUrl: './novo-usuario.component.html',
  styleUrls: ['./novo-usuario.component.scss']
})
export class NovoUsuarioComponent implements OnInit {

  usuario: UsuarioCadastro = {
    id: constantes.newGuid,
    nome: '',
    sobrenome: '',
    email: '',
    senha: '',
    confirmaSenha: '',
    ativo: true,
    regra: RegraUsuario.Usuario,
  };

  constructor(
    private usuarioApiService: UsuarioApiService,
    private router: Router,
    private autenticacaoService: AutenticacaoService,
    private tituloService: TituloService,
    private snackBar: MatSnackBar,
    public processandoService: ProcessandoService,
  ) { }

  ngOnInit() {
    this.tituloService.definir('Cadastre-se');
  }

  criar() {
    this.usuarioApiService.adicionar(this.usuario)
      .pipe(
        switchMap(() => this.autenticacaoService.autenticar({ email: this.usuario.email, senha: this.usuario.senha })),
      )
      .subscribe({
        next: () => this.router.navigate(['intro']),
        error: (error: HttpErrorResponse) => this.snackBar.open(error.message),
      });
  }

}
