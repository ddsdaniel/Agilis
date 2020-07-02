import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { CrudComponent } from 'src/app/components/crud-component';
import { constantes } from 'src/app/constants/constantes';
import { EscopoTime } from 'src/app/enums/pessoas/escopo-time.enum';
import { Time } from 'src/app/models/pessoas/time';
import { UsuarioFK } from 'src/app/models/pessoas/usuario-FK';
import { TimesApiService } from 'src/app/services/api/pessoas/times-api.service';
import { DialogoService } from 'src/app/services/dialogos/dialogo.service';
import { AutenticacaoService } from 'src/app/services/seguranca/autenticacao.service';

@Component({
  selector: 'app-times',
  templateUrl: './times.component.html',
  styleUrls: ['./times.component.scss']
})
export class TimesComponent extends CrudComponent<Time> {

  constructor(
    public timesApiService: TimesApiService,
    public snackBar: MatSnackBar,
    router: Router,
    private dialogoService: DialogoService,
    private autenticacaoService: AutenticacaoService,
  ) {
    super(timesApiService, snackBar, router, 'times');
  }

  adicionar() {
    this.dialogoService.abrirTexto('Entre com o nome do time', 'Nome')
      .subscribe(nome => {

        if (nome) {

          const admin: UsuarioFK = {
            id: this.autenticacaoService.usuarioLogado.usuario.id,
            nome: this.autenticacaoService.usuarioLogado.usuario.nome + '' +
              this.autenticacaoService.usuarioLogado.usuario.sobrenome,
            email: {
              endereco: this.autenticacaoService.usuarioLogado.usuario.email
            },
          };

          const time: Time = {
            id: constantes.newGuid,
            nome,
            colaboradores: [],
            administradores: [admin],
            escopo: EscopoTime.Colaborativo,
          };

          this.timesApiService.adicionar(time)
            .subscribe(
              () => super.atualizarDados(),
              (error: HttpErrorResponse) => this.snackBar.open(error.message)
            );

        }
      });
  }
}
