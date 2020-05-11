import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Time } from 'src/app/models/trabalho/times/time';
import { UsuarioApiService } from 'src/app/services/api/pessoas/usuario-api.service';
import { TimeApiService } from 'src/app/services/api/trabalho/time-api.service';

@Component({
  selector: 'app-times-form',
  templateUrl: './times-form.component.html',
  styleUrls: ['./times-form.component.scss']
})
export class TimesFormComponent implements OnInit {

  time: Time;

  constructor(
    private router: Router,
    private timeApiService: TimeApiService,
    private snackBar: MatSnackBar,
    private usuarioApiService: UsuarioApiService,
  ) { }

  ngOnInit() {
    this.time = {
      id: '00000000000000000000000000000000',
      nome: '',
      usuarioId: this.usuarioApiService.usuarioLogado.usuario.id
    };

  }

  salvar() {

    console.log(this.time);

    this.timeApiService.adicionar(this.time)
      .subscribe(
        (id: string) => this.router.navigateByUrl('times'),
        (error: HttpErrorResponse) => {
          console.log(error);
          this.snackBar.open(error.message);
        }
      );
  }

  cancelar() {
    // TODO: mater o estado da pesquisa
    this.router.navigateByUrl('times');
  }

}
