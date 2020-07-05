import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { CrudFormComponent } from 'src/app/components/crud-form-component';
import { constantes } from 'src/app/constants/constantes';
import { EscopoTime } from 'src/app/enums/pessoas/escopo-time.enum';
import { Time } from 'src/app/models/pessoas/time';
import { UsuarioFK } from 'src/app/models/pessoas/usuario-FK';
import { TimesApiService } from 'src/app/services/api/pessoas/times-api.service';
import { DialogoService } from 'src/app/services/dialogos/dialogo.service';
import { AutenticacaoService } from 'src/app/services/seguranca/autenticacao.service';

@Component({
  selector: 'app-times-form',
  templateUrl: './times-form.component.html',
  styleUrls: ['./times-form.component.scss']
})
export class TimesFormComponent extends CrudFormComponent<Time> implements OnInit {

  timeApiService: TimesApiService;

  constructor(
    router: Router,
    timeApiService: TimesApiService,
    snackBar: MatSnackBar,
    private dialogoService: DialogoService,
    protected activatedRoute: ActivatedRoute,
    private autenticacaoService: AutenticacaoService,
  ) {
    super(router, timeApiService, snackBar, activatedRoute, 'times');
    this.timeApiService = timeApiService;
    this.sugerirNovo();
  }

  ngOnInit() {
    this.activatedRoute.params.subscribe(
      params => {
        const id = this.activatedRoute.snapshot.paramMap.get('id');
        this.recuperarEntidade(id);
      }
    );
  }

  sugerirNovo() {
    this.entidade = {
      id: constantes.newGuid,
      nome: '',
      administradores: [],
      colaboradores: [],
      escopo: EscopoTime.Colaborativo,
      produtos: []
    };
  }

  podeEditar(): boolean {
    return this.entidade.escopo === EscopoTime.Colaborativo;
  }

  abrirDialogoColaborador() {
    this.dialogoService.abrirEmail()
      .subscribe(email => {
        if (email) {
          this.timeApiService.adicionarColaborador(this.entidade.id, email)
            .subscribe(
              (novoColab: UsuarioFK) => this.entidade.colaboradores.push(novoColab),
              (error: HttpErrorResponse) => this.snackBar.open(error.message)
            );
        }
      });
  }

  excluirColaborador(colabId: string) {
    this.timeApiService.excluirColaborador(this.entidade.id, colabId)
      .subscribe(
        () => {
          const index = this.entidade.colaboradores.findIndex(c => c.id === colabId);
          const usuarioExcluido = this.entidade.colaboradores.removeAt<UsuarioFK>(index)[0];

          const snackBarRef = this.snackBar.open('Excluído', 'Desfazer');

          snackBarRef.onAction().subscribe(() => {

            this.timeApiService.adicionarColaborador(this.entidade.id, usuarioExcluido.email)
              .subscribe(
                (novoColab) => this.entidade.colaboradores.insert(index, novoColab),
                (error: HttpErrorResponse) => this.snackBar.open(error.message)
              );

          });
        },
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

  abrirDialogoAdministrador() {
    this.dialogoService.abrirEmail()
      .subscribe(email => {
        if (email) {
          this.timeApiService.adicionarAdmin(this.entidade.id, email)
            .subscribe(
              (novoAdmin: UsuarioFK) => this.entidade.administradores.push(novoAdmin),
              (error: HttpErrorResponse) => this.snackBar.open(error.message)
            );
        }
      });
  }

  excluirAdministrador(adminId: string) {
    this.timeApiService.excluirAdmin(this.entidade.id, adminId)
      .subscribe(
        () => {
          const index = this.entidade.administradores.findIndex(c => c.id === adminId);
          const usuarioExcluido = this.entidade.administradores.removeAt<UsuarioFK>(index)[0];

          const snackBarRef = this.snackBar.open('Excluído', 'Desfazer');

          snackBarRef.onAction().subscribe(() => {

            this.timeApiService.adicionarAdmin(this.entidade.id, usuarioExcluido.email)
              .subscribe(
                (novoAdmin) => this.entidade.administradores.insert(index, novoAdmin),
                (error: HttpErrorResponse) => this.snackBar.open(error.message)
              );

          });
        },
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }
}
