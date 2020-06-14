import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { CrudFormComponent } from 'src/app/components/crud-form-component';
import { DialogoEmailComponent } from 'src/app/components/dialogo-email/dialogo-email.component';
import { constantes } from 'src/app/constants/constantes';
import { EscopoTime } from 'src/app/enums/pessoas/escopo-time.enum';
import { Email } from 'src/app/models/pessoas/email';
import { Time } from 'src/app/models/pessoas/time';
import { UsuarioVO } from 'src/app/models/pessoas/usuario-vo';
import { TimesApiService } from 'src/app/services/api/pessoas/times-api.service';
import { DialogoEmailService } from 'src/app/services/dialogos/dialogo-email.service';
import { AutenticacaoService } from 'src/app/services/seguranca/autenticacao.service';

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
    private dialogoEmailService: DialogoEmailService,
  ) {
    super(router, timeApiService, snackBar, activatedRoute, 'times');
    this.timeApiService = timeApiService;
    this.sugerirNovo();
  }

  abrirDialogoColaborador() {
    this.dialogoEmailService.abrir()
      .subscribe(email => {
        if (email) {
          this.timeApiService.adicionarColaborador(this.entidade.id, email)
            .subscribe(
              (novoColab: UsuarioVO) => this.entidade.colaboradores.push(novoColab),
              (error: HttpErrorResponse) => this.snackBar.open(error.message)
            );
        }
      });
  }

  podeEditar(): boolean {
    return this.entidade.escopo === EscopoTime.Colaborativo;
  }

  sugerirNovo(): void {
    this.entidade = {
      id: constantes.newGuid,
      nome: '',
      administradores: [{
        id: this.autenticacaoService.usuarioLogado.usuario.id,
        nome: this.autenticacaoService.usuarioLogado.usuario.nome + ' ' +
          this.autenticacaoService.usuarioLogado.usuario.sobrenome,
        email: {
          endereco: this.autenticacaoService.usuarioLogado.usuario.email
        }
      }],
      colaboradores: [],
      produtos: [],
      releases: [],
      escopo: EscopoTime.Colaborativo,
    };
  }

  excluirAdmin(adminId: string) {
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

  excluirColaborador(colabId: string) {
    this.timeApiService.excluirColaborador(this.entidade.id, colabId)
      .subscribe(
        () => {
          const index = this.entidade.colaboradores.findIndex(c => c.id === colabId);
          const usuarioExcluido = this.entidade.colaboradores.removeAt<UsuarioVO>(index)[0];

          const snackBarRef = this.snackBar.open('Excluído', 'Desfazer');

          snackBarRef.onAction().subscribe(() => {

            this.timeApiService.adicionarColaborador(this.entidade.id, usuarioExcluido.email)
              .subscribe(
                () => this.entidade.colaboradores.insert(index, usuarioExcluido),
                (error: HttpErrorResponse) => this.snackBar.open(error.message)
              );

          });
        },
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

}
