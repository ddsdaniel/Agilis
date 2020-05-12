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
  private listaTimes: Time[];

  constructor(
    private timeApiService: TimeApiService,
    private snackBar: MatSnackBar,
    private router: Router,
  ) { }

  ngOnInit() {
    this.atualizarTimes();
  }

  private atualizarTimes() {
    this.times = this.timeApiService.obteTodos();
    this.times.subscribe(
      (times: Time[]) => this.listaTimes = times,
      (error: HttpErrorResponse) => this.snackBar.open(error.message)
    );
  }

  adicionar() {
    this.router.navigateByUrl('times/new');
  }

  editar(id: string) {
    this.router.navigateByUrl('times/' + id);
  }

  excluir(index: number) {

    const time = this.listaTimes[index];

    this.timeApiService.excluir(time.id)
      .subscribe(
        () => {

          this.atualizarTimes();

          const snackBarRef = this.snackBar.open('Excluído', 'Desfazer');

          snackBarRef.onAction().subscribe(() => {

            this.timeApiService.adicionar(time)
              .subscribe(
                () => this.atualizarTimes(),
                (error: HttpErrorResponse) => this.snackBar.open(error.message)
              );

          });
        },
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }
}
