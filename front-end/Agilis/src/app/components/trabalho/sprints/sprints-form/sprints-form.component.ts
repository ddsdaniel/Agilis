﻿import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { CrudFormComponent } from 'src/app/components/crud-form-component';
import { constantes } from 'src/app/constants/constantes';
import { ReleaseFK } from 'src/app/models/trabalho/releases/release-fk';
import { Sprint } from 'src/app/models/trabalho/sprints/sprint';
import { ReleasesApiService } from 'src/app/services/api/trabalho/releases-api.service';
import { SprintsApiService } from 'src/app/services/api/trabalho/sprints-api.service';

@Component({
  selector: 'app-sprints-form',
  templateUrl: './sprints-form.component.html',
  styleUrls: ['./sprints-form.component.scss']
})
export class SprintsFormComponent extends CrudFormComponent<Sprint> implements OnInit {

  releases: ReleaseFK[];

  constructor(
    router: Router,
    sprintApiService: SprintsApiService,
    snackBar: MatSnackBar,
    activatedRoute: ActivatedRoute,
    private releasesApiService: ReleasesApiService,
  ) {
    super(router, sprintApiService, snackBar, activatedRoute, 'sprints');
    this.sugerirNovo();
  }

  ngOnInit() {
    this.releasesApiService.obterTodos()
      .subscribe(releases => this.releases = releases);

    super.ngOnInit();
  }

  sugerirNovo(): void {
    this.entidade = {
      id: constantes.newGuid,
      nome: '',
      periodo: {},
    };
  }

  salvar() {
    //this.entidade.release = this.releases.find(r => r.id === this.entidade.release.id);
    super.salvar();
  }
}
