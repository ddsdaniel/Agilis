import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { CrudComponent } from 'src/app/components/crud-component';
import { Time } from 'src/app/models/pessoas/time';
import { TimesApiService } from 'src/app/services/api/pessoas/times-api.service';
import { HttpErrorResponse } from '@angular/common/http';
import { EscopoTime } from 'src/app/enums/pessoas/escopo-time.enum';

@Component({
  selector: 'app-times',
  templateUrl: './times.component.html',
  styleUrls: ['./times.component.scss']
})
export class TimesComponent extends CrudComponent<Time> {

  // private timesApiService: TimesApiService;
  // private snackBar: MatSnackBar;

  constructor(
    public timesApiService: TimesApiService,
    public snackBar: MatSnackBar,
    router: Router,
  ) {
    super(timesApiService, snackBar, router, 'times');

    // this.timesApiService = timesApiService;
    // this.snackBar = snackBar;
  }
}
