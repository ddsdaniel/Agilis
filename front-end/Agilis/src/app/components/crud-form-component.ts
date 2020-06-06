import { HttpErrorResponse } from '@angular/common/http';
import { OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';

import { OperacaoFormCrud } from '../enums/operacao-form-crud.enum';
import { Entidade } from '../models/entidade';
import { CrudApiBaseService } from '../services/api/crud-api-base.service';

export abstract class CrudFormComponent<TEntity extends Entidade> implements OnInit {

  entidade: TEntity;
  operacao: OperacaoFormCrud;

  constructor(
    private router: Router,
    private apiService: CrudApiBaseService<TEntity>,
    protected snackBar: MatSnackBar,
    private activatedRoute: ActivatedRoute,
    private rotaPesquisa: string
  ) { }

  ngOnInit() {

    const id = this.activatedRoute.snapshot.paramMap.get('id');
    this.operacao = id ? OperacaoFormCrud.alterando : OperacaoFormCrud.adicionando;

    if (this.operacao === OperacaoFormCrud.adicionando) {
      this.sugerirNovo();
    } else {
      this.recuperarEntidade(id);
    }

  }

  recuperarEntidade(id: string): void {
    this.apiService.obterUm(id)
      .subscribe(
        (entidade: TEntity) => this.entidade = entidade,
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

  abstract sugerirNovo(): void;

  salvar(): void {

    if (this.operacao === OperacaoFormCrud.adicionando) {
      this.apiService.adicionar(this.entidade)
        .subscribe(
          (id: string) => this.router.navigateByUrl(this.rotaPesquisa),
          (error: HttpErrorResponse) => {
            console.log(error);
            this.snackBar.open(error.message);
          }
        );
    } else {
      this.apiService.alterar(this.entidade.id, this.entidade)
        .subscribe(
          () => this.router.navigateByUrl(this.rotaPesquisa),
          (error: HttpErrorResponse) => {
            console.log(error);
            this.snackBar.open(error.message);
          }
        );
    }
  }

  cancelar() {
    // TODO: mater o estado da pesquisa
    this.router.navigateByUrl(this.rotaPesquisa);
  }

}
