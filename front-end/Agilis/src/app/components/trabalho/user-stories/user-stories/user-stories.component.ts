import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { CrudComponent } from 'src/app/components/crud-component';
import { constantes } from 'src/app/constants/constantes';
import { Epico } from 'src/app/models/trabalho/epicos/epico';
import { UserStory } from 'src/app/models/trabalho/user-stories/user-story';
import { EpicosApiService } from 'src/app/services/api/trabalho/epicos-api.service';
import { UserStoriesApiService } from 'src/app/services/api/trabalho/user-stories-api.service';

@Component({
  selector: 'app-user-stories',
  templateUrl: './user-stories.component.html',
  styleUrls: ['./user-stories.component.scss']
})
export class UserStoriesComponent extends CrudComponent<UserStory> {

  epicoId: string = constantes.newGuid;
  epicos: Epico[];

  constructor(
    public userStoriesApiService: UserStoriesApiService,
    public snackBar: MatSnackBar,
    router: Router,
    private epicosApiService: EpicosApiService,
  ) {
    super(userStoriesApiService, snackBar, router, 'user-stories');
    this.carregarEpicos();
  }

  carregarEpicos() {
    this.epicosApiService.obterTodos()
      .subscribe(
        (epicos) => {
          const todos: Epico = {
            id: constantes.newGuid,
            nome: 'Todos',
            temaId: constantes.newGuid
          };
          epicos.insert(0, todos);
          this.epicos = epicos;
        },
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

  atualizarDados() {
    this.userStoriesApiService.pesquisarPorEpico(this.filtro, this.epicoId)
      .subscribe(
        (lista: UserStory[]) => this.lista = lista,
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

}
