import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { CrudComponent } from 'src/app/components/crud-component';
import { Tema } from 'src/app/models/trabalho/temas/tema';
import { Epico } from 'src/app/models/trabalho/epicos/epico';
import { TemasApiService } from 'src/app/services/api/trabalho/temas-api.service';
import { EpicosApiService } from 'src/app/services/api/trabalho/epicos-api.service';
import { constantes } from 'src/app/constants/constantes';

@Component({
  selector: 'app-epicos',
  templateUrl: './epicos.component.html',
  styleUrls: ['./epicos.component.scss']
})
export class EpicosComponent extends CrudComponent<Epico> {

  temaId: string = constantes.newGuid;
  temas: Tema[];

  constructor(
    public epicosApiService: EpicosApiService,
    public snackBar: MatSnackBar,
    router: Router,
    private temasApiService: TemasApiService,
  ) {
    super(epicosApiService, snackBar, router, 'epicos');
    this.carregarTemas();
  }

  carregarTemas() {
    this.temasApiService.obterTodos()
      .subscribe(
        (temas) => {
          const todos: Tema = {
            id: constantes.newGuid,
            nome: 'Todos',
            produtoId: constantes.newGuid,
            epicos: []
          };
          temas.insert(0, todos);
          this.temas = temas;
        },
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

  atualizarDados() {
    this.epicosApiService.pesquisarPorTema(this.filtro, this.temaId)
      .subscribe(
        (lista: Epico[]) => this.lista = lista,
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

}
