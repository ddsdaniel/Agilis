import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { CrudFormComponent } from 'src/app/components/crud-form-component';
import { constantes } from 'src/app/constants/constantes';
import { Tema } from 'src/app/models/trabalho/temas/tema';
import { Epico } from 'src/app/models/trabalho/epicos/epico';
import { TemasApiService } from 'src/app/services/api/trabalho/temas-api.service';
import { EpicosApiService } from 'src/app/services/api/trabalho/epicos-api.service';

@Component({
  selector: 'app-epicos-form',
  templateUrl: './epicos-form.component.html',
  styleUrls: ['./epicos-form.component.scss']
})
export class EpicosFormComponent extends CrudFormComponent<Epico> {

  temas: Tema[];
  epicoApiService: EpicosApiService;

  constructor(
    router: Router,
    epicoApiService: EpicosApiService,
    snackBar: MatSnackBar,
    protected activatedRoute: ActivatedRoute,
    private temasApiService: TemasApiService,
  ) {
    super(router, epicoApiService, snackBar, activatedRoute, 'epicos');
    this.epicoApiService = epicoApiService;
    this.carregarTemas();
    this.sugerirNovo();
  }

  carregarTemas() {
    this.temasApiService.obterTodos()
      .subscribe(
        (temas) => this.temas = temas,
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

  sugerirNovo() {
    this.entidade = {
      id: constantes.newGuid,
      nome: '',
      temaId: constantes.newGuid
    };
  }
}
