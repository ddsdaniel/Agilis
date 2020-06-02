import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { CrudFormComponent } from 'src/app/components/crud-form-component';
import { constantes } from 'src/app/constants/constantes';
import { Time } from 'src/app/models/pessoas/time';
import { Release } from 'src/app/models/trabalho/releases/release';
import { TimesApiService } from 'src/app/services/api/pessoas/times-api.service';
import { ReleasesApiService } from 'src/app/services/api/trabalho/releases-api.service';

@Component({
  selector: 'app-releases-form',
  templateUrl: './releases-form.component.html',
  styleUrls: ['./releases-form.component.scss']
})
export class ReleasesFormComponent extends CrudFormComponent<Release> implements OnInit {

  times: Time[];
  timeId: string;

  constructor(
    router: Router,
    releaseApiService: ReleasesApiService,
    snackBar: MatSnackBar,
    activatedRoute: ActivatedRoute,
    private timesApiService: TimesApiService,
  ) {
    super(router, releaseApiService, snackBar, activatedRoute, 'releases');
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
    };
  }

  salvar() {
    this.entidade.time = this.times.find(t => t.id === this.timeId);
    super.salvar();
  }

}
