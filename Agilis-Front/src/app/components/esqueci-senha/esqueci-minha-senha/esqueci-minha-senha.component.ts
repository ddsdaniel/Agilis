import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Email } from 'src/app/models/email';
import { UsuarioApiService } from 'src/app/services/apis/usuario-api.service';
import { ProcessandoService } from 'src/app/services/processando.service';
import { TituloService } from 'src/app/services/titulo.service';

@Component({
  selector: 'app-esqueci-minha-senha',
  templateUrl: './esqueci-minha-senha.component.html',
  styleUrls: ['./esqueci-minha-senha.component.scss']
})
export class EsqueciMinhaSenhaComponent implements OnInit {

  email: Email = {
    endereco: ''
  };

  constructor(
    private tituloService: TituloService,
    private usuarioApiService: UsuarioApiService,
    private snackBar: MatSnackBar,
    private router: Router,
    public processandoService: ProcessandoService,
  ) { }

  ngOnInit(): void {
    this.tituloService.definir('Esqueci a Minha Senha');
  }


  recuperar() {
    this.usuarioApiService.esqueciMinhaSenha(this.email)
      .subscribe({
        next: () => {
          this.snackBar.open('Verifique a sua caixa de entrada de e-mails.');
          this.router.navigate(['bem-vindo']);
        },
        error: (error: HttpErrorResponse) => this.snackBar.open(error.message)
      });
  }

}
