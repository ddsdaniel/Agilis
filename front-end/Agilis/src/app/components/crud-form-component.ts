import { HttpErrorResponse } from '@angular/common/http';
import { OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';

import { Entidade } from '../models/entidade';
import { CrudApiBaseService } from '../services/api/crud-api-base.service';

export abstract class CrudFormComponent<TEntity extends Entidade> implements OnInit {

  entidade: TEntity;

  constructor(
    protected router: Router,
    private apiService: CrudApiBaseService<TEntity>,
    protected snackBar: MatSnackBar,
    protected activatedRoute: ActivatedRoute,
    private rotaPesquisa: string
  ) { }

  ngOnInit() {

    this.activatedRoute.params.subscribe(
      params => {
        const id = this.activatedRoute.snapshot.paramMap.get('id');
        this.recuperarEntidade(id);
      }
    );

  }

  recuperarEntidade(id: string): void {
    this.apiService.obterUm(id)
      .subscribe(
        (entidade: TEntity) => {
          this.entidade = entidade;
          this.recuperouEntidade(entidade);
        },
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

  recuperouEntidade(entidade: TEntity) {

  }

  salvar(): void {
    this.apiService.alterar(this.entidade.id, this.entidade)
      .subscribe(
        () => this.router.navigateByUrl(this.rotaPesquisa),
        (error: HttpErrorResponse) => {
          console.log(error);
          this.snackBar.open(error.message);
        }
      );
  }

  cancelar() {
    // TODO: mater o estado da pesquisa
    this.router.navigateByUrl(this.rotaPesquisa);
  }

}
