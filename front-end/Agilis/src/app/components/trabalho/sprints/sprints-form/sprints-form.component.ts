import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { CrudFormComponent } from 'src/app/components/crud-form-component';
import { constantes } from 'src/app/constants/constantes';
import { Sprint } from 'src/app/models/trabalho/sprints/sprint';
import { SprintsApiService } from 'src/app/services/api/trabalho/sprints-api.service';

@Component({
  selector: 'app-sprints-form',
  templateUrl: './sprints-form.component.html',
  styleUrls: ['./sprints-form.component.scss']
})
export class SprintsFormComponent extends CrudFormComponent<Sprint> {

  constructor(
    router: Router,
    sprintApiService: SprintsApiService,
    snackBar: MatSnackBar,
    activatedRoute: ActivatedRoute,
  ) {
    super(router, sprintApiService, snackBar, activatedRoute, 'sprints');
    this.sugerirNovo();
  }

  sugerirNovo(): void {
    this.entidade = {
      id: constantes.newGuid,
      nome: '',
      periodo: {},
    };
  }

  salvar() {
    super.salvar();
  }
}
