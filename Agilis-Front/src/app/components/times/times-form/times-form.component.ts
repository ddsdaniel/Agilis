import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { constantes } from 'src/app/consts/constantes';
import { Time } from 'src/app/models/time';
import { TimeApiService } from 'src/app/services/apis/time-api.service';
import { ComparadorService } from 'src/app/services/comparador.service';
import { TituloService } from 'src/app/services/titulo.service';
import { CrudFormComponent } from '../../crud/crud-form-component';

@Component({
  selector: 'app-times-form',
  templateUrl: './times-form.component.html',
  styleUrls: ['./times-form.component.scss']
})
export class TimesFormComponent extends CrudFormComponent<Time> implements OnInit {


  constructor(
    router: Router,
    timeApiService: TimeApiService,
    snackBar: MatSnackBar,
    tituloService: TituloService,
    protected activatedRoute: ActivatedRoute,
    public comparadorService: ComparadorService,
  ) {
    super(router, timeApiService, snackBar, activatedRoute, 'times');
    tituloService.definir('Cadastro do Time');
  }

  sugerirNovo(): void {
    this.entidade = {
      id: constantes.newGuid,
      nome: '',
    };
  }

  testarTimeFixo(): boolean {
    return this.entidade.nome === constantes.nomeDefault;
  }
}
