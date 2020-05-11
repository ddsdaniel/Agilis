import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserStory } from 'src/app/models/trabalho/user-stories/user-story';
import { UserStoryApiService } from 'src/app/services/api/trabalho/user-story-api.service';
import { HttpErrorResponse } from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Observable } from 'rxjs';
import { Ator } from 'src/app/models/pessoas/ator';
import { AtorApiService } from 'src/app/services/api/pessoas/ator-api.service';

@Component({
  selector: 'app-user-stories-form',
  templateUrl: './user-stories-form.component.html',
  styleUrls: ['./user-stories-form.component.scss']
})
export class UserStoriesFormComponent implements OnInit {

  userStory: UserStory;
  atores: Observable<Ator[]>;

  constructor(
    private router: Router,
    private userStoryApiService: UserStoryApiService,
    private snackBar: MatSnackBar,
    private atorApiService: AtorApiService,
  ) { }

  ngOnInit() {
    this.userStory = {
      id: '',
      nome: '',
      ator: {
        id: '',
        nome: ''
      },
      narrativa: '',
      objetivo: '',
      historia: '',
    };

    this.atores = this.atorApiService.obteTodos();
    this.atores.subscribe(atores => this.userStory.ator.id = atores[0].id);
  }

  salvar() {

    console.log(this.userStory);

    this.userStoryApiService.adicionar(this.userStory)
      .subscribe(
        (id: string) => this.router.navigateByUrl('user-stories'),
        (error: HttpErrorResponse) => {
          console.log(error);
          this.snackBar.open(error.message);
        }
      );
  }

  cancelar() {
    // TODO: mater o estado da pesquisa
    this.router.navigateByUrl('user-stories');
  }

}
