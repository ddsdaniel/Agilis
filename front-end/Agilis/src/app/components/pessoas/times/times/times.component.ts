import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { CrudComponent } from 'src/app/components/crud-component';
import { Time } from 'src/app/models/pessoas/time';
import { TimesApiService } from 'src/app/services/api/pessoas/times-api.service';

@Component({
  selector: 'app-times',
  templateUrl: './times.component.html',
  styleUrls: ['./times.component.scss']
})
export class TimesComponent extends CrudComponent<Time> {

  constructor(
    public timesApiService: TimesApiService,
    public snackBar: MatSnackBar,
    router: Router,
  ) {
    super(timesApiService, snackBar, router, 'times');
  }
}
