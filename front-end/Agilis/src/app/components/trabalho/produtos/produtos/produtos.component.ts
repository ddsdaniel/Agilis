import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { CrudComponent } from 'src/app/components/crud-component';
import { Time } from 'src/app/models/pessoas/time';
import { Produto } from 'src/app/models/trabalho/produtos/produto';
import { TimesApiService } from 'src/app/services/api/pessoas/times-api.service';
import { ProdutosApiService } from 'src/app/services/api/trabalho/produtos-api.service';
import { constantes } from 'src/app/constants/constantes';
import { EscopoTime } from 'src/app/enums/pessoas/escopo-time.enum';

@Component({
  selector: 'app-produtos',
  templateUrl: './produtos.component.html',
  styleUrls: ['./produtos.component.scss']
})
export class ProdutosComponent extends CrudComponent<Produto> {

  timeId: string = constantes.newGuid;
  times: Time[];

  constructor(
    public produtosApiService: ProdutosApiService,
    public snackBar: MatSnackBar,
    router: Router,
    private timesApiService: TimesApiService,
  ) {
    super(produtosApiService, snackBar, router, 'produtos');
    this.carregarTimes();
  }

  carregarTimes() {
    this.timesApiService.obterTodos()
      .subscribe(
        (times) => {
          const todos: Time = {
            id: constantes.newGuid,
            nome: 'Todos',
            administradores: [],
            colaboradores: [],
            escopo: EscopoTime.Colaborativo
          };
          times.insert(0, todos);
          this.times = times;
        },
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

  atualizarDados() {
    this.produtosApiService.pesquisarPorTime(this.filtro, this.timeId)
      .subscribe(
        (lista: Produto[]) => this.lista = lista,
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

}
