import { HttpErrorResponse } from '@angular/common/http';
import { OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';

import { Entidade } from '../models/entidade';
import { CrudApiBaseService } from '../services/api/crud-api-base.service';

export class CrudComponent<TEntity extends Entidade> implements OnInit {

  lista: TEntity[];
  filtro = '';

  constructor(
    public apiService: CrudApiBaseService<TEntity>,
    public snackBar: MatSnackBar,
    public router: Router,
    private rota: string,
  ) { }

  ngOnInit() {
    this.atualizarDados();
  }

  atualizarDados() {
    console.log('filtro: ' + this.filtro);

    if (this.filtro) {
      this.apiService.pesquisar(this.filtro)
        .subscribe(
          (lista: TEntity[]) => this.lista = lista,
          (error: HttpErrorResponse) => this.snackBar.open(error.message)
        );
    } else {
      this.apiService.obterTodos()
        .subscribe(
          (lista: TEntity[]) => this.lista = lista,
          (error: HttpErrorResponse) => this.snackBar.open(error.message)
        );
    }
  }

  adicionar() {
    this.router.navigateByUrl(`${this.rota}/new`);
  }

  editar(id: string) {
    this.router.navigateByUrl(`${this.rota}/${id}`);
  }

  excluir(index: number) {

    const entidade = this.lista[index];

    this.apiService.excluir(entidade.id)
      .subscribe(
        () => {

          this.lista.removeAt(index);

          const snackBarRef = this.snackBar.open('Excluído', 'Desfazer');

          snackBarRef.onAction().subscribe(() => {

            this.apiService.adicionar(entidade)
              .subscribe(
                () => this.lista.insert(index, entidade),
                (error: HttpErrorResponse) => this.snackBar.open(error.message)
              );

          });
        },
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }
}
