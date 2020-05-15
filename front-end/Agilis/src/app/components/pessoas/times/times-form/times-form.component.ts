import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { constantes } from 'src/app/constants/constantes';
import { OperacaoFormCrud } from 'src/app/enums/operacao-form-crud.enum';
import { Time } from 'src/app/models/pessoas/time';
import { UsuariosApiService } from 'src/app/services/api/pessoas/usuarios-api.service';
import { TimesApiService } from 'src/app/services/api/pessoas/times-api.service';
import { CrudFormComponent } from 'src/app/components/crud-form-component';

@Component({
  selector: 'app-times-form',
  templateUrl: './times-form.component.html',
  styleUrls: ['./times-form.component.scss']
})
export class TimesFormComponent extends CrudFormComponent<Time> {

  constructor(
    router: Router,
    timeApiService: TimesApiService,
    snackBar: MatSnackBar,
    activatedRoute: ActivatedRoute,
    private usuariosApiService: UsuariosApiService,
  ) {
    super(router, timeApiService, snackBar, activatedRoute, 'times');
  }

  sugerirNovo(): void {
    this.entidade = {
      id: constantes.newGuid,
      nome: '',
      usuarioId: this.usuariosApiService.usuarioLogado.usuario.id,
      favorito: false
    };
  }

}
