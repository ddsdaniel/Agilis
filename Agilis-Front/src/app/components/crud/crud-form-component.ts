import { HttpErrorResponse } from '@angular/common/http';
import { Directive, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { OperacaoFormCrud } from 'src/app/enums/operacao-form-crud.enum';
import { Entidade } from 'src/app/models/entidade';
import { CrudApiBaseService } from 'src/app/services/apis/crud-api-base.service';

// @Directive()
@Directive()
export abstract class CrudFormComponent<TEntity extends Entidade> implements OnInit {

  entidade: TEntity;
  operacao: OperacaoFormCrud;

  constructor(
    protected router: Router,
    private apiService: CrudApiBaseService<TEntity>,
    protected snackBar: MatSnackBar,
    protected activatedRoute: ActivatedRoute,
    protected rotaPesquisa: string
  ) { }

  ngOnInit() {

    this.sugerirNovo();

    this.activatedRoute.params.subscribe(
      params => {

        const id = this.activatedRoute.snapshot.paramMap.get('id');

        this.operacao = id && id !== 'new'
          ? OperacaoFormCrud.alterando
          : OperacaoFormCrud.adicionando;

        if (this.operacao === OperacaoFormCrud.alterando) {
          this.recuperarEntidade(id);
        }

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

  abstract sugerirNovo(): void;

  salvar(): void {

    if (this.operacao === OperacaoFormCrud.adicionando) {
      this.apiService.adicionar(this.entidade)
        .subscribe(
          (id: string) => this.navegarParaPesquisa(),
          (error: HttpErrorResponse) => {
            this.snackBar.open(error.message);
          }
        );
    } else {
      this.apiService.alterar(this.entidade.id, this.entidade)
        .subscribe(
          () => this.navegarParaPesquisa(),
          (error: HttpErrorResponse) => {
            this.snackBar.open(error.message);
          }
        );
    }
  }

  cancelar() {
    this.navegarParaPesquisa();
  }

  navegarParaPesquisa() {
    this.router.navigateByUrl(this.rotaPesquisa);
  }

}
