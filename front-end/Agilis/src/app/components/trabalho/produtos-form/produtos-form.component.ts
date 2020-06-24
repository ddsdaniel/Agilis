import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Jornada } from 'src/app/models/trabalho/produtos/jornada';
import { Produto } from 'src/app/models/trabalho/produtos/produto';
import { ProdutosApiService } from 'src/app/services/api/trabalho/produtos-api.service';
import { DialogoService } from 'src/app/services/dialogos/dialogo.service';

@Component({
  selector: 'app-produtos-form',
  templateUrl: './produtos-form.component.html',
  styleUrls: ['./produtos-form.component.scss']
})
export class ProdutosFormComponent  implements OnInit {

  private timeId: string;
  produto: Produto;

  constructor(
    private router: Router,
    private produtoApiService: ProdutosApiService,
    private snackBar: MatSnackBar,
    private activatedRoute: ActivatedRoute,
    private dialogoService: DialogoService,
  ) { }

  ngOnInit() {
    this.activatedRoute.params.subscribe(
      params => {
        this.timeId = this.activatedRoute.snapshot.paramMap.get('timeId');
        const produtoId = this.activatedRoute.snapshot.paramMap.get('produtoId');

        this.produtoApiService.obterUm(produtoId)
          .subscribe(
            (entidade: Produto) => this.produto = entidade,
            (error: HttpErrorResponse) => this.snackBar.open(error.message)
          );

      }
    );
  }

  renomear() {
    this.produtoApiService.renomear(this.timeId, this.produto.id, this.produto.nome)
      .subscribe(
        () => this.router.navigateByUrl(`times/${this.timeId}`),
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

  abrirDialogoJornada() {
    this.dialogoService.abrirTexto('Entre com o nome da Jornada', 'Nome')
      .subscribe(nome => {
        if (nome) {
          this.produtoApiService.adicionarJornada(this.produto.id, nome)
            .subscribe(
              (novaJornada: Jornada) => this.produto.jornadas.push(novaJornada),
              (error: HttpErrorResponse) => this.snackBar.open(error.message)
            );
        }
      });
  }

  drop(event: CdkDragDrop<string[]>) {
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      transferArrayItem(event.previousContainer.data,
                        event.container.data,
                        event.previousIndex,
                        event.currentIndex);
    }
  }

  excluirJornada(jornadaId: string) {
    this.produtoApiService.excluirJornada(this.produto.id, jornadaId)
    .subscribe(
      () => {
        const index = this.produto.jornadas.findIndex(c => c.id === jornadaId);
        const jornadaExcluido = this.produto.jornadas.removeAt<Jornada>(index)[0];

        const snackBarRef = this.snackBar.open('Excluído', 'Desfazer');

        snackBarRef.onAction().subscribe(() => {

          this.produtoApiService.adicionarJornada(this.produto.id, jornadaExcluido.nome)
            .subscribe(
              (novoJornada) => this.produto.jornadas.insert(index, novoJornada),
              (error: HttpErrorResponse) => this.snackBar.open(error.message)
            );

        });
      },
      (error: HttpErrorResponse) => this.snackBar.open(error.message)
    );
  }
}
