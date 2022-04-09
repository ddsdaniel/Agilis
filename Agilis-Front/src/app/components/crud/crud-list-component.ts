import { HttpErrorResponse } from '@angular/common/http';
import { Directive, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Entidade } from 'src/app/models/entidade';
import { CrudApiBaseService } from 'src/app/services/apis/crud-api-base.service';

@Directive()
export class CrudListComponent<TEntity extends Entidade> implements OnInit {

  lista: TEntity[] = [];
  listaCompleta: TEntity[];

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
    this.apiService.obterTodos()
      .subscribe(
        (lista: TEntity[]) => {
          this.lista = lista;
          this.listaCompleta = lista;
          this.atualizouLista();
        },
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
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
          this.atualizouLista();

          const snackBarRef = this.snackBar.open('Excluído', 'Desfazer');

          snackBarRef.onAction().subscribe(() => {

            this.apiService.adicionar(entidade)
              .subscribe(
                () => {
                  this.lista.insert(index, entidade);
                  this.atualizouLista();
                },
                (error: HttpErrorResponse) => this.snackBar.open(error.message)
              );

          });
        },
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

  atualizouLista() {

  }
}
