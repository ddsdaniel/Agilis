﻿import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { CrudFormComponent } from 'src/app/components/crud-form-component';
import { constantes } from 'src/app/constants/constantes';
import { EscopoTime } from 'src/app/enums/pessoas/escopo-time.enum';
import { Time } from 'src/app/models/pessoas/time';
import { UsuarioFK } from 'src/app/models/pessoas/usuario-FK';
import { ProdutoFK } from 'src/app/models/trabalho/produtos/produto-fk';
import { ReleaseFK } from 'src/app/models/trabalho/releases/release-fk';
import { TimesApiService } from 'src/app/services/api/pessoas/times-api.service';
import { DialogoService } from 'src/app/services/dialogos/dialogo.service';
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
    private dialogoService: DialogoService,
  ) {
    super(router, timeApiService, snackBar, activatedRoute, 'times');
    this.timeApiService = timeApiService;
    this.sugerirNovo();
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

  abrirDialogoProduto() {
    this.dialogoService.abrirTexto('Entre com o nome do produto', 'Nome')
    .subscribe(nome => {
      if (nome) {
        this.timeApiService.adicionarProduto(this.entidade.id, nome)
          .subscribe(
            (novoProduto: ProdutoFK) => this.entidade.produtos.push(novoProduto),
            (error: HttpErrorResponse) => this.snackBar.open(error.message)
          );
      }
    });
  }

  excluirProduto(produtoId: string) {
    this.timeApiService.excluirProduto(this.entidade.id, produtoId)
    .subscribe(
      () => {
        const index = this.entidade.produtos.findIndex(p => p.id === produtoId);
        const produtoExcluido = this.entidade.produtos.removeAt<ProdutoFK>(index)[0];

        const snackBarRef = this.snackBar.open('Excluído', 'Desfazer');

        snackBarRef.onAction().subscribe(() => {

          this.timeApiService.adicionarProduto(this.entidade.id, produtoExcluido.nome)
            .subscribe(
              (novoProduto) => this.entidade.produtos.insert(index, novoProduto),
              (error: HttpErrorResponse) => this.snackBar.open(error.message)
            );

        });
      },
      (error: HttpErrorResponse) => this.snackBar.open(error.message)
    );
  }

  abrirDialogoRelease() {
    this.dialogoService.abrirTexto('Entre com o nome da Release', 'Nome')
    .subscribe(nome => {
      if (nome) {
        this.timeApiService.adicionarRelease(this.entidade.id, nome)
          .subscribe(
            (novaRelease: ReleaseFK) => this.entidade.releases.push(novaRelease),
            (error: HttpErrorResponse) => this.snackBar.open(error.message)
          );
      }
    });
  }

  excluirRelease(releaseId: string) {
    this.timeApiService.excluirRelease(this.entidade.id, releaseId)
    .subscribe(
      () => {
        const index = this.entidade.releases.findIndex(p => p.id === releaseId);
        const releaseExcluida = this.entidade.releases.removeAt<ReleaseFK>(index)[0];

        const snackBarRef = this.snackBar.open('Excluída', 'Desfazer');

        snackBarRef.onAction().subscribe(() => {

          this.timeApiService.adicionarRelease(this.entidade.id, releaseExcluida.nome)
            .subscribe(
              (novoRelease) => this.entidade.releases.insert(index, novoRelease),
              (error: HttpErrorResponse) => this.snackBar.open(error.message)
            );

        });
      },
      (error: HttpErrorResponse) => this.snackBar.open(error.message)
    );
  }
}
