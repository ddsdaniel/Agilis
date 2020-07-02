import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { UserStory } from 'src/app/models/trabalho/user-stories/user-story';
import { UserStoryApiService } from 'src/app/services/api/trabalho/user-stories-api.service';
import { HttpErrorResponse } from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Observable } from 'rxjs';
import { Ator } from 'src/app/models/pessoas/ator';
import { AtoresApiService } from 'src/app/services/api/pessoas/atores-api.service';

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
    private atorApiService: AtoresApiService,
    private activatedRoute: ActivatedRoute,
  ) { }

  ngOnInit() {

    const id = this.activatedRoute.snapshot.paramMap.get('id');
    //this.operacao = id ? OperacaoFormCrud.alterando : OperacaoFormCrud.adicionando;

    this.userStory = {
      id: id ? id : '',
      nome: '',
      ator: {
        id: '',
        nome: ''
      },
      narrativa: '',
      objetivo: '',
      historia: '',
    };

    this.atores = this.atorApiService.obterTodos();
    this.atores.subscribe(atores => this.userStory.ator.id = atores[0].id);
  }

  salvar() {

    // if (this.operacao === OperacaoFormCrud.adicionando) {
    //   this.userStoryApiService.adicionar(this.userStory)
    //     .subscribe(
    //       (id: string) => this.router.navigateByUrl('user-stories'),
    //       (error: HttpErrorResponse) => {
    //         console.log(error);
    //         this.snackBar.open(error.message);
    //       }
    //     );
    // } else {
      this.userStoryApiService.alterar(this.userStory.id, this.userStory)
        .subscribe(
          () => this.router.navigateByUrl('user-stories'),
          (error: HttpErrorResponse) => {
            console.log(error);
            this.snackBar.open(error.message);
          }
        );
   // }
  }

  cancelar() {
    // TODO: mater o estado da pesquisa
    this.router.navigateByUrl('user-stories');
  }

}
