import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { constantes } from 'src/app/consts/constantes';
import { Sprint } from 'src/app/models/sprint';
import { SprintApiService } from 'src/app/services/apis/sprint-api.service';
import { ComparadorService } from 'src/app/services/comparador.service';
import { TituloService } from 'src/app/services/titulo.service';
import { CrudFormComponent } from '../../crud/crud-form-component';

@Component({
  selector: 'app-sprints-form',
  templateUrl: './sprints-form.component.html',
  styleUrls: ['./sprints-form.component.scss']
})
export class SprintsFormComponent extends CrudFormComponent<Sprint> implements OnInit {


  constructor(
    router: Router,
    sprintApiService: SprintApiService,
    snackBar: MatSnackBar,
    tituloService: TituloService,
    protected activatedRoute: ActivatedRoute,
    public comparadorService: ComparadorService,
  ) {
    super(router, sprintApiService, snackBar, activatedRoute, 'sprints');
    tituloService.definir('Cadastro do Sprint');
  }

  sugerirNovo(): void {
    this.entidade = {
      id: constantes.newGuid,
      nome: '',
      objetivos: '',
    };
  }

}
