import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { CrudFormComponent } from 'src/app/components/crud-form-component';
import { constantes } from 'src/app/constants/constantes';
import { Epico } from 'src/app/models/trabalho/epicos/epico';
import { UserStory } from 'src/app/models/trabalho/user-stories/user-story';
import { EpicosApiService } from 'src/app/services/api/trabalho/epicos-api.service';
import { UserStoriesApiService } from 'src/app/services/api/trabalho/user-stories-api.service';

@Component({
  selector: 'app-user-stories-form',
  templateUrl: './user-stories-form.component.html',
  styleUrls: ['./user-stories-form.component.scss']
})
export class UserStoriesFormComponent extends CrudFormComponent<UserStory> {

  epicos: Epico[];
  userStoryApiService: UserStoriesApiService;

  constructor(
    router: Router,
    userStoryApiService: UserStoriesApiService,
    snackBar: MatSnackBar,
    protected activatedRoute: ActivatedRoute,
    private epicosApiService: EpicosApiService,
  ) {
    super(router, userStoryApiService, snackBar, activatedRoute, 'user-stories');
    this.userStoryApiService = userStoryApiService;
    this.carregarEpicos();
    this.sugerirNovo();
  }

  carregarEpicos() {
    this.epicosApiService.obterTodos()
      .subscribe(
        (epicos) => this.epicos = epicos,
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

  sugerirNovo() {
    this.entidade = {
      id: constantes.newGuid,
      nome: '',
      epicoId: constantes.newGuid,
      ator: {
        id: constantes.newGuid,
        nome: '',
        produtoId: constantes.newGuid
      },
      historia: '',
      narrativa: '',
      objetivo: ''
    };
  }
}
