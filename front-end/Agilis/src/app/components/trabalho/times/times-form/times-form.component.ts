import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { OperacaoFormCrud } from 'src/app/enums/operacao-form-crud.enum';
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
  operacao: OperacaoFormCrud;

  constructor(
    private router: Router,
    private timeApiService: TimeApiService,
    private snackBar: MatSnackBar,
    private usuarioApiService: UsuarioApiService,
    private activatedRoute: ActivatedRoute,
  ) { }

  ngOnInit() {

    const id = this.activatedRoute.snapshot.paramMap.get('timeId');
    this.operacao = id ? OperacaoFormCrud.alterando : OperacaoFormCrud.adicionando;

    if (this.operacao === OperacaoFormCrud.adicionando) {
      this.sugerirNovo();
    } else {
      this.recuperarTime(id);
    }

  }

  recuperarTime(id: string) {
    this.timeApiService.obterUm(id)
    .subscribe(
      (time: Time) => this.time = time,
      (error: HttpErrorResponse) => this.snackBar.open(error.message)
    );
  }

  sugerirNovo() {
    this.time =  {
      id: '00000000000000000000000000000000',
      nome: '',
      usuarioId: this.usuarioApiService.usuarioLogado.usuario.id
    };
  }

  salvar() {

    if (this.operacao === OperacaoFormCrud.adicionando) {
      this.timeApiService.adicionar(this.time)
        .subscribe(
          (id: string) => this.router.navigateByUrl('times'),
          (error: HttpErrorResponse) => {
            console.log(error);
            this.snackBar.open(error.message);
          }
        );
    } else {
      this.timeApiService.alterar(this.time.id, this.time)
        .subscribe(
          () => this.router.navigateByUrl('times'),
          (error: HttpErrorResponse) => {
            console.log(error);
            this.snackBar.open(error.message);
          }
        );
    }
  }

  cancelar() {
    // TODO: mater o estado da pesquisa
    this.router.navigateByUrl('times');
  }

}
