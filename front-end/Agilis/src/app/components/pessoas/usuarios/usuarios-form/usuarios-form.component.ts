import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { UsuarioCadastro } from 'src/app/models/pessoas/usuario-cadastro';
import { UsuarioLogado } from 'src/app/models/pessoas/usuario-logado';
import { LoginDados } from 'src/app/models/seguranca/login-dados';
import { UsuariosApiService } from 'src/app/services/api/pessoas/usuarios-api.service';
import { AutenticacaoService } from 'src/app/services/seguranca/autenticacao.service';
import { RegraUsuario } from 'src/app/enums/pessoas/regra-usuario.enum';

@Component({
  selector: 'app-usuarios-form',
  templateUrl: './usuarios-form.component.html',
  styleUrls: ['./usuarios-form.component.scss']
})
export class UsuariosFormComponent implements OnInit {

  usuario: UsuarioCadastro;

  constructor(
    private router: Router,
    private autenticacaoService: AutenticacaoService,
    private snackBar: MatSnackBar,
    private usuariosApiService: UsuariosApiService,
  ) { }

  ngOnInit() {
    this.sugerirNovo();
  }

  sugerirNovo(): void {
    this.usuario = {
      nome: '',
      sobrenome: '',
      email: '',
      confirmaSenha: '',
      senha: '',
      regra: RegraUsuario.Usuario
    };
  }

  salvar(): void {

    this.usuariosApiService.cadastrar(this.usuario)
      .subscribe(
        (id: string) => this.autenticar(),
        (error: HttpErrorResponse) => {
          console.log(error);
          this.snackBar.open(error.message);
        }
      );

  }

  autenticar(): void {

    const login: LoginDados = {
      email: this.usuario.email,
      senha: this.usuario.senha
    };

    this.autenticacaoService.autenticar(login)
      .subscribe(
        () => this.router.navigate(['times']),
        (error: HttpErrorResponse) => {
          console.log(error);
          this.snackBar.open(error.message);
        }
      );
  }

  cancelar() {
    this.router.navigateByUrl('login');
  }

}
