import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { CrudFormComponent } from 'src/app/components/crud-form-component';
import { constantes } from 'src/app/constants/constantes';
import { TimeVO } from 'src/app/models/pessoas/time-vo';
import { Produto } from 'src/app/models/trabalho/produtos/produto';
import { TimesApiService } from 'src/app/services/api/pessoas/times-api.service';
import { ProdutosApiService } from 'src/app/services/api/trabalho/produtos-api.service';

@Component({
  selector: 'app-produtos-form',
  templateUrl: './produtos-form.component.html',
  styleUrls: ['./produtos-form.component.scss']
})
export class ProdutosFormComponent extends CrudFormComponent<Produto> implements OnInit {

  times: TimeVO[];

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
      time: {
        id: constantes.newGuid,
        nome: ''
      },
    };
  }

  salvar() {
    this.entidade.time = this.times.find(t => t.id === this.entidade.time.id);
    super.salvar();
  }
}
