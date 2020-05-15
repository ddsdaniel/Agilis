import { HttpErrorResponse } from '@angular/common/http';
import { OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';

import { EntidadeFavorita } from '../models/entidade-favorita';
import { Favorito } from '../models/favorito';
import { CrudFavoritoApiBaseService } from '../services/api/crud-favorito-api-base.service';

export class CrudComponent<TEntity extends EntidadeFavorita> implements OnInit {

  lista: TEntity[];
  filtro = '';

  constructor(
    private apiService: CrudFavoritoApiBaseService<TEntity>,
    private snackBar: MatSnackBar,
    private router: Router,
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

  favoritar(id: string, marcado: boolean) {

    const favorito: Favorito = {
      marcado
    };

    this.apiService.favoritar(id, favorito)
      .subscribe(
        () => {
          const entidade = this.lista.find(t => t.id === id);
          entidade.favorito = marcado;
        },
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }
}
