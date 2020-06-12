import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { CrudComponent } from 'src/app/components/crud-component';
import { Sprint } from 'src/app/models/trabalho/sprints/sprint';
import { SprintsApiService } from 'src/app/services/api/trabalho/sprints-api.service';

@Component({
  selector: 'app-sprints',
  templateUrl: './sprints.component.html',
  styleUrls: ['./sprints.component.scss']
})
export class SprintsComponent extends CrudComponent<Sprint> {

  constructor(
    public sprintsApiService: SprintsApiService,
    public snackBar: MatSnackBar,
    router: Router,
  ) {
    super(sprintsApiService, snackBar, router, 'sprints');
  }
}
