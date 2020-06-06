import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { CrudFormComponent } from 'src/app/components/crud-form-component';
import { constantes } from 'src/app/constants/constantes';
import { EscopoTime } from 'src/app/enums/pessoas/escopo-time.enum';
import { Email } from 'src/app/models/pessoas/email';
import { Time } from 'src/app/models/pessoas/time';
import { TimesApiService } from 'src/app/services/api/pessoas/times-api.service';
import { AutenticacaoService } from 'src/app/services/seguranca/autenticacao.service';
import { UsuarioVO } from 'src/app/models/pessoas/usuario-vo';

@Component({
  selector: 'app-times-form',
  templateUrl: './times-form.component.html',
  styleUrls: ['./times-form.component.scss']
})
export class TimesFormComponent extends CrudFormComponent<Time> {

  emailAdmin: string;
  timeApiService: TimesApiService;

  constructor(
    router: Router,
    timeApiService: TimesApiService,
    snackBar: MatSnackBar,
    activatedRoute: ActivatedRoute,
    private autenticacaoService: AutenticacaoService,
  ) {
    super(router, timeApiService, snackBar, activatedRoute, 'times');
    this.timeApiService = timeApiService;
    this.sugerirNovo();
  }

  sugerirNovo(): void {
    this.entidade = {
      id: constantes.newGuid,
      nome: '',
      administradores: [{
        id: this.autenticacaoService.usuarioLogado.usuario.id,
        nome: this.autenticacaoService.usuarioLogado.usuario.nome + ' ' +
          this.autenticacaoService.usuarioLogado.usuario.sobrenome
      }],
      colaboradores: [],
      produtos: [],
      escopo: EscopoTime.Colaborativo,
    };
  }

  excluirAdmin(adminId: string){
    this.timeApiService.excluirAdmin(this.entidade.id, adminId)
      .subscribe(
        () => {
          const index = this.entidade.administradores.findIndex(a => a.id === adminId);
          this.entidade.administradores.removeAt(index);
        },
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

  adicionarAdmin() {
    const email: Email = {
      endereco: this.emailAdmin
    };
    this.timeApiService.adicionarAdmin(this.entidade.id, email)
      .subscribe(
        (novoAdmin: UsuarioVO) => {
          this.entidade.administradores.push(novoAdmin);
          this.emailAdmin = '';
        },
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }
}
