import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Time } from 'src/app/models/trabalho/times/time';
import { TimeApiService } from 'src/app/services/api/trabalho/time-api.service';

@Component({
  selector: 'app-times',
  templateUrl: './times.component.html',
  styleUrls: ['./times.component.scss']
})
export class TimesComponent implements OnInit {

  times: Observable<Time[]>;

  constructor(
    private userStoryApiService: TimeApiService,
    private snackBar: MatSnackBar,
    private router: Router,
  ) { }

  ngOnInit() {
    this.times = this.userStoryApiService.obteTodos();
    this.times.subscribe(
      () => { },
      (error: HttpErrorResponse) => this.snackBar.open(error.message)
    );
  }

  adicionar() {
    this.router.navigateByUrl('times/new');
  }

  editar(id: string){
    this.router.navigateByUrl('times/' + id);
  }
}
