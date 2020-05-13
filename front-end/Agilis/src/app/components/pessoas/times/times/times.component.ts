import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Favorito } from 'src/app/models/favorito';
import { Time } from 'src/app/models/pessoas/time';
import { TimeApiService } from 'src/app/services/api/trabalho/time-api.service';

@Component({
  selector: 'app-times',
  templateUrl: './times.component.html',
  styleUrls: ['./times.component.scss']
})
export class TimesComponent implements OnInit {

  times: Time[];

  constructor(
    private timeApiService: TimeApiService,
    private snackBar: MatSnackBar,
    private router: Router,
  ) { }

  ngOnInit() {
    this.atualizarTimes();
  }

  private atualizarTimes() {
    this.timeApiService.obteTodos()
      .subscribe(
        (times: Time[]) => this.times = times,
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

    const time = this.times[index];

    this.timeApiService.excluir(time.id)
      .subscribe(
        () => {

          this.times.removeAt(index);

          const snackBarRef = this.snackBar.open('Excluído', 'Desfazer');

          snackBarRef.onAction().subscribe(() => {

            this.timeApiService.adicionar(time)
              .subscribe(
                () => this.times.insert(index, time),
                (error: HttpErrorResponse) => this.snackBar.open(error.message)
              );

          });
        },
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

  favoritar(id: string, marcado: boolean) {

    const favorito: Favorito = {
      marcado
    };

    this.timeApiService.favoritar(id, favorito)
      .subscribe(
        () => {
          const time = this.times.find(t => t.id === id);
          time.favorito = marcado;
        },
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }
}
