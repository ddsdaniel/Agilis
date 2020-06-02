import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { CrudComponent } from 'src/app/components/crud-component';
import { Release } from 'src/app/models/trabalho/releases/release';
import { ReleasesApiService } from 'src/app/services/api/trabalho/releases-api.service';

@Component({
  selector: 'app-releases',
  templateUrl: './releases.component.html',
  styleUrls: ['./releases.component.scss']
})
export class ReleasesComponent extends CrudComponent<Release> {

  constructor(
    public releasesApiService: ReleasesApiService,
    public snackBar: MatSnackBar,
    router: Router,
  ) {
    super(releasesApiService, snackBar, router, 'releases');
  }
}
