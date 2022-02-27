import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { AlterarMinhaSenha } from 'src/app/models/alterar-minha-senha';
import { UsuarioApiService } from 'src/app/services/apis/usuario-api.service';
import { ProcessandoService } from 'src/app/services/processando.service';
import { TituloService } from 'src/app/services/titulo.service';

@Component({
  selector: 'app-alterar-minha-senha',
  templateUrl: './alterar-minha-senha.component.html',
  styleUrls: ['./alterar-minha-senha.component.scss']
})
export class AlterarMinhaSenhaComponent implements OnInit {

  alterarMinhaSenha: AlterarMinhaSenha = {
    senhaAtual: '',
    novaSenha: '',
    confirmaSenha: '',
  };

  constructor(
    private usuarioApiService: UsuarioApiService,
    private snackBar: MatSnackBar,
    private router: Router,
    private tituloService: TituloService,
    public processandoService: ProcessandoService,
  ) { }

  ngOnInit(): void {
    this.tituloService.definir('Alterar a Minha Senha');
  }

  salvar() {
    this.usuarioApiService.alterarMinhaSenha(this.alterarMinhaSenha)
      .subscribe({
        next: () => {
          this.snackBar.open('Senha alterada com sucesso.');
          this.router.navigate(['/transacoes']);
        },
        error: (error) => this.snackBar.open(error.message),
      });
  }
}
