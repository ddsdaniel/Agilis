﻿import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { CrudFormComponent } from 'src/app/components/crud-form-component';
import { constantes } from 'src/app/constants/constantes';
import { Ator } from 'src/app/models/pessoas/ator';
import { Epico } from 'src/app/models/trabalho/epicos/epico';
import { CriterioAceitacao } from 'src/app/models/trabalho/user-stories/criterio-aceitacao';
import { UserStory } from 'src/app/models/trabalho/user-stories/user-story';
import { AtoresApiService } from 'src/app/services/api/pessoas/atores-api.service';
import { EpicosApiService } from 'src/app/services/api/trabalho/epicos-api.service';
import { UserStoriesApiService } from 'src/app/services/api/trabalho/user-stories-api.service';
import { DialogoService } from 'src/app/services/dialogos/dialogo.service';

@Component({
  selector: 'app-user-stories-form',
  templateUrl: './user-stories-form.component.html',
  styleUrls: ['./user-stories-form.component.scss']
})
export class UserStoriesFormComponent extends CrudFormComponent<UserStory> {

  epicos: Epico[];
  atores: Ator[];

  userStoryApiService: UserStoriesApiService;

  constructor(
    router: Router,
    userStoryApiService: UserStoriesApiService,
    snackBar: MatSnackBar,
    protected activatedRoute: ActivatedRoute,
    private epicosApiService: EpicosApiService,
    private atoresApiService: AtoresApiService,
    private dialogoService: DialogoService,
  ) {
    super(router, userStoryApiService, snackBar, activatedRoute, 'user-stories');
    this.userStoryApiService = userStoryApiService;
    this.carregarEpicos();
    this.carregarAtores();
    this.sugerirNovo();
  }

  carregarAtores() {
    this.atoresApiService.obterTodos()
      .subscribe(
        (atores) => this.atores = atores,
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

  carregarEpicos() {
    this.epicosApiService.obterTodos()
      .subscribe(
        (epicos) => this.epicos = epicos,
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

  salvar(): void {
    this.entidade.ator.nome = this.atores.find(a => a.id === this.entidade.ator.id).nome;
    console.log(this.entidade);
    super.salvar();
  }

  sugerirNovo() {
    this.entidade = {
      id: constantes.newGuid,
      nome: '',
      epicoId: constantes.newGuid,
      ator: {
        id: constantes.newGuid,
        nome: '',
      },
      historia: '',
      narrativa: '',
      objetivo: '',
      criteriosAceitacao: []
    };
  }

  abrirDialogoCriterio() {
    this.dialogoService.abrirTexto('Entre com o nome do critério', 'Nome do critério')
      .subscribe(nome => {
        if (nome) {
          const criterio: CriterioAceitacao = {
            nome
          };
          this.entidade.criteriosAceitacao.push(criterio);
        }
      });
  }

  excluirCriterio(index: number) {
    const excluido = this.entidade.criteriosAceitacao.removeAt<CriterioAceitacao>(index)[0];
    const snackBarRef = this.snackBar.open('Excluído', 'Desfazer');

    snackBarRef.onAction().subscribe(() => {
      this.entidade.criteriosAceitacao.insert(index, excluido);
    });
  }

}
