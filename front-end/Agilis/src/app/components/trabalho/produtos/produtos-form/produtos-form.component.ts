import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { CrudFormComponent } from 'src/app/components/crud-form-component';
import { constantes } from 'src/app/constants/constantes';
import { TimeVO } from 'src/app/models/pessoas/time-vo';
import { Produto } from 'src/app/models/trabalho/produtos/produto';
import { Sprint } from 'src/app/models/trabalho/sprints/sprint';
import { SprintVO } from 'src/app/models/trabalho/sprints/sprint-vo';
import { TimesApiService } from 'src/app/services/api/pessoas/times-api.service';
import { ProdutosApiService } from 'src/app/services/api/trabalho/produtos-api.service';

@Component({
  selector: 'app-produtos-form',
  templateUrl: './produtos-form.component.html',
  styleUrls: ['./produtos-form.component.scss']
})
export class ProdutosFormComponent extends CrudFormComponent<Produto> implements OnInit {

  numeroSprint: number;
  nomeSprint: string;
  times: TimeVO[];
  produtoApiService: ProdutosApiService;

  constructor(
    router: Router,
    produtoApiService: ProdutosApiService,
    snackBar: MatSnackBar,
    activatedRoute: ActivatedRoute,
    private timesApiService: TimesApiService,
  ) {
    super(router, produtoApiService, snackBar, activatedRoute, 'times');
    this.produtoApiService = produtoApiService;
    this.sugerirNovo();
  }

  ngOnInit() {
    this.timesApiService.obterTodos()
      .subscribe(times => this.times = times);

    super.ngOnInit();
  }

  sugerirNovo(): void {
    this.entidade = {
      id: constantes.newGuid,
      nome: '',
      time: {
        id: constantes.newGuid,
        nome: ''
      },
      sprints: [],
    };
  }

  salvar() {
    this.entidade.time = this.times.find(t => t.id === this.entidade.time.id);
    super.salvar();
  }

  excluirSprint(sprintId: string) {
    this.produtoApiService.excluirSprint(this.entidade.id, sprintId)
      .subscribe(
        () => {
          const index = this.entidade.sprints.findIndex(s => s.id === sprintId);
          this.entidade.sprints.removeAt(index);
        },
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

  adicionarSprint() {
    const sprint: Sprint = {
      id: constantes.newGuid,
      nome: this.nomeSprint,
      release: {
        id: this.entidade.id,
        nome: this.entidade.nome
      },
      numero: this.numeroSprint,
      periodo: {}
    };
    this.produtoApiService.adicionarSprint(this.entidade.id, sprint)
      .subscribe(
        (novoSprint: SprintVO) => {
          this.entidade.sprints.push(novoSprint);
          this.nomeSprint = '';
          this.numeroSprint = 0;
        },
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

  editarSprint(id: string) {
    this.router.navigateByUrl(`sprints/${id}`);
  }
}
