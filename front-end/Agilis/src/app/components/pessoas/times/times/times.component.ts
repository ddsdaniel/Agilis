import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { CrudComponent } from 'src/app/components/crud-component';
import { Time } from 'src/app/models/pessoas/time';
import { TimeApiService } from 'src/app/services/api/pessoas/time-api.service';

@Component({
  selector: 'app-times',
  templateUrl: './times.component.html',
  styleUrls: ['./times.component.scss']
})
export class TimesComponent extends CrudComponent<Time> {

  constructor(
    timeApiService: TimeApiService,
    snackBar: MatSnackBar,
    router: Router,
  ) {
    super(timeApiService, snackBar, router, 'times');
  }

}
