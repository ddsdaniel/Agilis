import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { CrudFormComponent } from 'src/app/components/crud-form-component';
import { constantes } from 'src/app/constants/constantes';
import { Time } from 'src/app/models/pessoas/time';
import { Produto } from 'src/app/models/trabalho/produtos/produto';
import { TimesApiService } from 'src/app/services/api/pessoas/times-api.service';
import { ProdutosApiService } from 'src/app/services/api/trabalho/produtos-api.service';

@Component({
  selector: 'app-produtos-form',
  templateUrl: './produtos-form.component.html',
  styleUrls: ['./produtos-form.component.scss']
})
export class ProdutosFormComponent extends CrudFormComponent<Produto> {

  times: Time[];
  produtoApiService: ProdutosApiService;
  sugestaoTimeId: string = constantes.newGuid;

  constructor(
    router: Router,
    produtoApiService: ProdutosApiService,
    snackBar: MatSnackBar,
    protected activatedRoute: ActivatedRoute,
    private timesApiService: TimesApiService,
  ) {
    super(router, produtoApiService, snackBar, activatedRoute, 'produtos');
    this.recuperarQueryParams();
    this.produtoApiService = produtoApiService;
    this.carregarTimes();
    this.sugerirNovo();
  }

  recuperarQueryParams() {
    this.activatedRoute.queryParams
      .subscribe(params => {
        if (params.timeId) {
          this.sugestaoTimeId = params.timeId;
          super.rotaPesquisa = 'times/' + this.sugestaoTimeId;
        }
      });
  }

  carregarTimes() {
    this.timesApiService.obterTodos()
      .subscribe(
        (times) => this.times = times,
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

  sugerirNovo() {
    this.entidade = {
      id: constantes.newGuid,
      nome: '',
      timeId: this.sugestaoTimeId,
      atores: [],
      storyMapping: {
        temas: []
      }
    };
  }
}
