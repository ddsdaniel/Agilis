import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
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
export class ProdutosFormComponent extends CrudFormComponent<Produto> implements OnInit {

  times: Time[];
  timeId: string;

  constructor(
    router: Router,
    produtoApiService: ProdutosApiService,
    snackBar: MatSnackBar,
    activatedRoute: ActivatedRoute,
    private timesApiService: TimesApiService,
  ) {
    super(router, produtoApiService, snackBar, activatedRoute, 'produtos');
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
      time: null,
      favorito: false,
    };
  }

  salvar() {
    this.entidade.time = this.times.find(t => t.id === this.timeId);
    super.salvar();
  }

}
