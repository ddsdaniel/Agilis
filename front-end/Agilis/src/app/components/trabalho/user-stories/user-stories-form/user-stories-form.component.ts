import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { CrudFormComponent } from 'src/app/components/crud-form-component';
import { constantes } from 'src/app/constants/constantes';
import { Ator } from 'src/app/models/pessoas/ator';
import { CriterioAceitacao } from 'src/app/models/trabalho/user-stories/criterio-aceitacao';
import { UserStory } from 'src/app/models/trabalho/user-stories/user-story';
import { AtoresApiService } from 'src/app/services/api/pessoas/atores-api.service';
import { UserStoriesApiService } from 'src/app/services/api/trabalho/user-stories-api.service';
import { DialogoService } from 'src/app/services/dialogos/dialogo.service';
import { TituloService } from 'src/app/services/titulo.service';

@Component({
  selector: 'app-user-stories-form',
  templateUrl: './user-stories-form.component.html',
  styleUrls: ['./user-stories-form.component.scss']
})
export class UserStoriesFormComponent extends CrudFormComponent<UserStory> {

  atores: Ator[];
  userStoryApiService: UserStoriesApiService;
  private produtoId: string;

  constructor(
    router: Router,
    userStoryApiService: UserStoriesApiService,
    snackBar: MatSnackBar,
    protected activatedRoute: ActivatedRoute,
    private atoresApiService: AtoresApiService,
    private dialogoService: DialogoService,
    private tituloService: TituloService,
  ) {
    super(router, userStoryApiService, snackBar, activatedRoute, 'produtos');
    this.userStoryApiService = userStoryApiService;
    this.recuperarQueryParams();
    this.carregarAtores();
    this.sugerirNovo();
  }

  recuperarQueryParams() {
    this.activatedRoute.queryParams
      .subscribe(params => {
        if (params.produtoId) {
          this.produtoId = params.produtoId;
          super.rotaPesquisa = 'story-mapping/' + this.produtoId;
        }
      });
  }

  recuperouEntidade(userStory: UserStory){
    this.tituloService.setTitulo(`${userStory.nome} - User Story`);
  }

  carregarAtores() {
    this.atoresApiService.obterTodos()
      .subscribe(
        (atores) => this.atores = atores,
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

  salvar(): void {
    this.entidade.ator.nome = this.atores.find(a => a.id === this.entidade.ator.id).nome;
    super.salvar();
  }

  sugerirNovo() {
    this.entidade = {
      id: constantes.newGuid,
      nome: '',
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
